using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Deletar;

public class UsuarioDeletarHandler(IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<UsuarioDeletarRequest, CommandResponse<UsuarioDeletarResponse>>
{
    public async Task<CommandResponse<UsuarioDeletarResponse>> Handle(UsuarioDeletarRequest request,
        CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(false, c => c.Id == request.Id, false, cancellationToken);
        if (usuario == null)
        {
            AddNotification("Usuario", UsuarioResources.UsuarioNaoEncontrado);
            return new CommandResponse<UsuarioDeletarResponse>(this);
        }

        AddNotifications(usuario.Notifications);

        if (IsInvalid())
            return new CommandResponse<UsuarioDeletarResponse>(this);

        repositoryUsuario.DeleteAsync(usuario);

        var response = new UsuarioDeletarResponse(usuario.Id, UsuarioResources.UsuarioDeletadoComSucesso);
        return new CommandResponse<UsuarioDeletarResponse>(response, this);
    }
}