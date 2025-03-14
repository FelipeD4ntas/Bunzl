using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Bunzl.Domain.Commands.Usuario.ResetSenha;

public class UsuarioResetSenhaHandler(IConfiguration configuration, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<UsuarioResetSenhaRequest, CommandResponse<UsuarioResetSenhaResponse>>
{
    public async Task<CommandResponse<UsuarioResetSenhaResponse>> Handle(UsuarioResetSenhaRequest request, CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(true, p => p.ChaveResetSenha == request.Chave, false, cancellationToken);
        if (usuario == null)
        {
            AddNotification("Usuario", UsuarioResources.ChaveResetSenhaInvalida);
            return new CommandResponse<UsuarioResetSenhaResponse>(this);
        }

        var chaveExpiraEmHoras = Convert.ToInt32(configuration["User:TimeHoursExpireKeyResetPassword"]);
        var diferenca = DateTime.UtcNow - usuario.DataChaveResetSenha!.Value;
        if (diferenca.TotalHours > chaveExpiraEmHoras)
        {
            AddNotification("Usuario", UsuarioResources.ChaveResetSenhaExpirada);
            return new CommandResponse<UsuarioResetSenhaResponse>(this);
        }

        if (usuario.Senha == request.NovaSenha.EncryptPassword())
        {
            AddNotification("Usuario", UsuarioResources.UsuarioSenhaDeveSerDiferente);
            return new CommandResponse<UsuarioResetSenhaResponse>(this);
        }

        usuario.AlterarSenha(request.NovaSenha);
        usuario.LimparChaveResetSenha();

        repositoryUsuario.Update(usuario);

        var response = new UsuarioResetSenhaResponse(UsuarioResources.ResetSenhaExecutadoComSucesso);
        return new CommandResponse<UsuarioResetSenhaResponse>(response, this);
    }
}