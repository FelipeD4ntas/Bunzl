using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.SolicitarResetSenha;

public class UsuarioSolicitarResetSenhaHandler(IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<UsuarioSolicitarResetSenhaRequest, CommandResponse<UsuarioSolicitarResetSenhaResponse>>
{
    public async Task<CommandResponse<UsuarioSolicitarResetSenhaResponse>> Handle(
        UsuarioSolicitarResetSenhaRequest request,
        CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(false, c => c.Email == request.Email, false, cancellationToken);
        if (usuario == null)
        {
            AddNotification("Usuario", UsuarioResources.UsuarioNaoEncontrado);
            return new CommandResponse<UsuarioSolicitarResetSenhaResponse>(this);
        }

        usuario.GerarChaveResetSenha();
        if (usuario.ChaveResetSenha == null || usuario.DataChaveResetSenha == null)
        {
            AddNotification("ChaveResetSenha", UsuarioResources.FalhaGerarChaveReset);
            return new CommandResponse<UsuarioSolicitarResetSenhaResponse>(this);
        }

        repositoryUsuario.Update(usuario);

        var response = new UsuarioSolicitarResetSenhaResponse(usuario.Id, UsuarioResources.ChaveResetGeradaComSucesso, usuario.Nome, usuario.Email, usuario.ChaveResetSenha.Value);
        return new CommandResponse<UsuarioSolicitarResetSenhaResponse>(response, this);
    }
}