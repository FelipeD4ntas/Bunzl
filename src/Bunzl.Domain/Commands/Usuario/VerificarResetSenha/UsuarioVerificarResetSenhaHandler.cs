using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Bunzl.Domain.Commands.Usuario.VerificarResetSenha;

public class UsuarioVerificarResetSenhaHandler(IConfiguration configuration, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<UsuarioVerificarResetSenhaRequest, CommandResponse<UsuarioVerificarResetSenhaResponse>>
{
    public async Task<CommandResponse<UsuarioVerificarResetSenhaResponse>> Handle(UsuarioVerificarResetSenhaRequest request, CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(false, p => p.ChaveResetSenha == request.Chave, false, cancellationToken);
        if (usuario == null)
        {
            AddNotification("Usuario", UsuarioResources.ChaveResetSenhaInvalida);
            return new CommandResponse<UsuarioVerificarResetSenhaResponse>(this);
        }

        var chaveExpiraEmHoras = Convert.ToInt32(configuration["User:TimeHoursExpireKeyResetPassword"]);
        var diferenca = DateTime.UtcNow - usuario.DataChaveResetSenha!.Value;
        if (diferenca.TotalHours > chaveExpiraEmHoras)
        {
            AddNotification("Usuario", UsuarioResources.ChaveResetSenhaExpirada);
            return new CommandResponse<UsuarioVerificarResetSenhaResponse>(this);
        }

        var response = new UsuarioVerificarResetSenhaResponse(UsuarioResources.ChaveResetSenhaValida);
        return new CommandResponse<UsuarioVerificarResetSenhaResponse>(response, this);
    }
}