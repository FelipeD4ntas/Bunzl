using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.Inativar;

public class FornecedorInativarHandler(IPublisher mediator, IRepositoryFornecedor repositoryFornecedor, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<FornecedorInativarRequest, CommandResponse<FornecedorInativarResponse>>
{
    public async Task<CommandResponse<FornecedorInativarResponse>> Handle(FornecedorInativarRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(false, (c) => c.Id == request.Id, false, cancellationToken, c => c.Usuarios);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorInativarResponse>(this);
        }

        var usuario = fornecedor.Usuarios.FirstOrDefault();
        if (usuario is not null)
        {
            fornecedor.Inativar();
            usuario.Inativar();

            fornecedor.Usuarios = [];
            usuario.Fornecedores = [];

            repositoryFornecedor.Update(fornecedor);
            repositoryUsuario.Update(usuario);
        }

        AddNotifications(fornecedor.Notifications);

        if (IsInvalid())
            return new CommandResponse<FornecedorInativarResponse>(this);

        await mediator.Publish(new AuditoriaAdicionarInput(fornecedor.Id, TabelasResources.Fornecedor, "Inativado", ETipoAuditoria.Modificado));

        return new CommandResponse<FornecedorInativarResponse>(new FornecedorInativarResponse(fornecedor.Id), this);
    }
}
