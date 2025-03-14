using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Inativar;

public class UsuarioInativarHandler(IPublisher mediator, IRepositoryUsuario repositoryUsuario, IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<UsuarioInativarRequest, CommandResponse<UsuarioInativarResponse>>
{
    public async Task<CommandResponse<UsuarioInativarResponse>> Handle(UsuarioInativarRequest request, CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(false, (c) => c.Id == request.Id, false, cancellationToken, u => u.Fornecedores);
        if (usuario == null)
        {
            AddNotification("Usuario", UsuarioResources.UsuarioNaoEncontrado);
            return new CommandResponse<UsuarioInativarResponse>(this);
        }

        usuario.Inativar();

        if (usuario.PerfilPermissao == EPerfilUsuario.FornecedorEndUser)
        {
            var fornecedor = usuario.Fornecedores.FirstOrDefault();

            if (fornecedor is not null)
            {
                fornecedor.Inativar();

                fornecedor.Usuarios = [];
                usuario.Fornecedores = [];

                repositoryFornecedor.Update(fornecedor);
            }
        }

        AddNotifications(usuario.Notifications);

        if (IsInvalid())
            return new CommandResponse<UsuarioInativarResponse>(this);

        repositoryUsuario.Update(usuario);
        await mediator.Publish(new AuditoriaAdicionarInput(usuario.Id, TabelasResources.Usuario, "Inativado", ETipoAuditoria.Modificado));

        return new CommandResponse<UsuarioInativarResponse>(new UsuarioInativarResponse(usuario.Id), this);
    }
}
