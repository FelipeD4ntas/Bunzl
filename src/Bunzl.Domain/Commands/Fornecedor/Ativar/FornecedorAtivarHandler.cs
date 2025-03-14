using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.Ativar;

public class FornecedorAtivarHandler(IPublisher mediator, IRepositoryFornecedor repositoryFornecedor, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<FornecedorAtivarRequest, CommandResponse<FornecedorAtivarResponse>>
{
    public async Task<CommandResponse<FornecedorAtivarResponse>> Handle(FornecedorAtivarRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(false, (c) => c.Id == request.Id, false, cancellationToken, c => c.Usuarios);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorAtivarResponse>(this);
        }

        var usuario = fornecedor.Usuarios.FirstOrDefault();
        if (usuario is not null)
        {
            fornecedor.Ativar();
            usuario.Ativar();
            fornecedor.Status = EStatusFornecedor.NaoHomologado;

            fornecedor.Usuarios = [];
            usuario.Fornecedores = [];

            repositoryFornecedor.Update(fornecedor);
            repositoryUsuario.Update(usuario);
        }

        AddNotifications(fornecedor.Notifications);

        if (IsInvalid())
            return new CommandResponse<FornecedorAtivarResponse>(this);

        repositoryFornecedor.Update(fornecedor);
        await mediator.Publish(new AuditoriaAdicionarInput(fornecedor.Id, TabelasResources.Fornecedor, "Ativado", ETipoAuditoria.Modificado));

        return new CommandResponse<FornecedorAtivarResponse>(new FornecedorAtivarResponse(fornecedor.Id), this);
    }
}