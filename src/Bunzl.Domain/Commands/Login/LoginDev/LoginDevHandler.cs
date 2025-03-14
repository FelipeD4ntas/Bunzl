using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Token;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Bunzl.Domain.Commands.Login.LoginDev;

public class LoginDevHandler(IConfiguration configuration, IRepositoryUsuario repositoryUsuario) : Notifiable, IRequestHandler<LoginDevRequest, CommandResponse<LoginDevResponse>>
{
    public async Task<CommandResponse<LoginDevResponse>> Handle(LoginDevRequest request, CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(true, p => p.Email == "luis.s@lyncas.net" && p.FlagAtivo == true, cancellationToken, p => p.Empresas)
            ?? await repositoryUsuario.GetByAsync(true, p => p.PerfilPermissao == EPerfilUsuario.BunzlCorporativoMasterUser && p.FlagAtivo == true, cancellationToken, p => p.Empresas);

        if (usuario is null)
        {
            AddNotification("Usuário", LoginResources.LoginNaoIdentificado);
            return await Task.FromResult(new CommandResponse<LoginDevResponse>(this));
        }

        usuario.UltimoLogin = DateTime.UtcNow;
        if (usuario.DataPrimeiroLogin == null)
            usuario.DataPrimeiroLogin = DateTime.UtcNow;

        var token = new TokenBuilder(configuration)
            .WithUserId(usuario.Id.ToString())
            .WithUserName(usuario.Nome)
            .WithUserCompany(usuario.Empresas.OrderBy(x => x.Nome).First().Id.ToString())
            .WithProfile(usuario.PerfilPermissao.ToString())
            .Build();

        return new CommandResponse<LoginDevResponse>(new LoginDevResponse(usuario.Nome, usuario.Email, token), this);
    }
}