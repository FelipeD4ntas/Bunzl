using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Ativar;

public class UsuarioAtivarHandler(IRepositoryUsuario repositoryUsuario, IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<UsuarioAtivarRequest, CommandResponse<UsuarioAtivarResponse>>
{
    public async Task<CommandResponse<UsuarioAtivarResponse>> Handle(UsuarioAtivarRequest request,
        CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(false, c => c.Id == request.Id, false, cancellationToken, c => c.Fornecedores);
        if (usuario == null)
        {
            AddNotification("Usuario", UsuarioResources.UsuarioNaoEncontrado);
            return new CommandResponse<UsuarioAtivarResponse>(this);
        }

        if (usuario.PerfilPermissao == EPerfilUsuario.FornecedorEndUser)
        {
            var fornecedor = usuario.Fornecedores.FirstOrDefault();

            if (fornecedor is not null)
            {
                fornecedor.Ativar();
                fornecedor.Status = EStatusFornecedor.NaoHomologado;

                fornecedor.Usuarios = [];
                usuario.Fornecedores = [];

                repositoryFornecedor.Update(fornecedor);
            }
        }

        usuario.Ativar();
        AddNotifications(usuario.Notifications);

        if (IsInvalid())
            return new CommandResponse<UsuarioAtivarResponse>(this);

        repositoryUsuario.Update(usuario);

        return new CommandResponse<UsuarioAtivarResponse>(new UsuarioAtivarResponse(usuario.Id), this);
    }
}