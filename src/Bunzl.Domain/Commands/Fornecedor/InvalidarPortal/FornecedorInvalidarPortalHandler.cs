using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.InvalidarPortal;

public class FornecedorInvalidarPortalHandler(IPublisher mediator, IRepositoryFornecedor repositoryFornecedor, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<FornecedorInvalidarPortalRequest, CommandResponse<FornecedorInvalidarPortalResponse>>
{
    public async Task<CommandResponse<FornecedorInvalidarPortalResponse>> Handle(FornecedorInvalidarPortalRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(false, (c) => c.Id == request.Id, false, cancellationToken, c => c.Usuarios);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorInvalidarPortalResponse>(this);
        }

        var usuario = fornecedor.Usuarios.FirstOrDefault();
        if (usuario is not null)
        {
            fornecedor.InvalidarPortal();
            usuario.Inativar();

            fornecedor.Usuarios = [];
            usuario.Fornecedores = [];

            repositoryFornecedor.Update(fornecedor);
            repositoryUsuario.Update(usuario);
        }

        AddNotifications(fornecedor.Notifications);

        if (IsInvalid())
            return new CommandResponse<FornecedorInvalidarPortalResponse>(this);

        await mediator.Publish(new AuditoriaAdicionarInput(fornecedor.Id, TabelasResources.Fornecedor, "Inativado", ETipoAuditoria.Modificado));

        return new CommandResponse<FornecedorInvalidarPortalResponse>(new FornecedorInvalidarPortalResponse(fornecedor.Id), this);
    }
}
