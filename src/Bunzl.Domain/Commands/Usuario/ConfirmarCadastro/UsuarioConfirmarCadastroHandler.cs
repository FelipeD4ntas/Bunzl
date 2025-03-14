using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.ConfirmarCadastro;

public class UsuarioConfirmarCadastroHandler(IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<UsuarioConfirmarCadastroRequest, CommandResponse<UsuarioConfirmarCadastroResponse>>
{
    public async Task<CommandResponse<UsuarioConfirmarCadastroResponse>> Handle(UsuarioConfirmarCadastroRequest request, CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(true, p => p.ChaveCadastro == request.ChaveCadastro,false, cancellationToken);
        if (usuario == null)
        {
            AddNotification("ChaveCadastro", UsuarioResources.ChaveCadastroNaoEncontrada);
            return new CommandResponse<UsuarioConfirmarCadastroResponse>(this);
        }

        usuario.Telefone = request.Telefone;
        usuario.AlterarSenha(request.NovaSenha);
        usuario.Area = request.Area;
        usuario.ChaveCadastro = null;

        if (IsInvalid())
            return new CommandResponse<UsuarioConfirmarCadastroResponse>(this);

        repositoryUsuario.Update(usuario);

        return new CommandResponse<UsuarioConfirmarCadastroResponse>(
            new UsuarioConfirmarCadastroResponse(UsuarioResources.UsuarioConfirmadoComSucesso),
            this);
    }
}