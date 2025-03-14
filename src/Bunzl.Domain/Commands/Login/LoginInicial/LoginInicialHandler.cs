using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Bunzl.Domain.Commands.Login.LoginInicial;

public class LoginInicialHandler(IConfiguration configuration, IRepositoryUsuario repositoryUsuario) : Notifiable, IRequestHandler<LoginInicialRequest, CommandResponse<LoginInicialResponse>>
{
    public async Task<CommandResponse<LoginInicialResponse>> Handle(LoginInicialRequest request, CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(false, p => p.Email == request.Email.Trim(), cancellationToken, p => p.Empresas);
        if (usuario == null)
        {
            AddNotification("Usuario", LoginResources.LoginNaoIdentificado);
            return await Task.FromResult(new CommandResponse<LoginInicialResponse>(this));
        }

        if (!usuario.FlagAtivo)
        {
            AddNotification("Usuário", LoginResources.LoginUsuarioInvativo);
            return await Task.FromResult(new CommandResponse<LoginInicialResponse>(this));
        }

        if (usuario.Senha == null)
        {
            AddNotification("Usuario", LoginResources.LoginCadastroNaoFinalizado);
            return await Task.FromResult(new CommandResponse<LoginInicialResponse>(this));
        }

        if (usuario.Empresas == null || !usuario.Empresas.Any())
        {
            AddNotification("Usuário", LoginResources.LoginUsuarioSemEmpresa);
            return await Task.FromResult(new CommandResponse<LoginInicialResponse>(this));
        }

        if (usuario.Senha != request.Senha.EncryptPassword())
        {
            AddNotification("Usuario", LoginResources.LoginSenhaInvalido);
            return await Task.FromResult(new CommandResponse<LoginInicialResponse>(this));
        }

        if (usuario.PerfilPermissao == Enumerators.EPerfilUsuario.FornecedorEndUser && usuario.UltimoLogin != null)
        {
            var loginFornecedorExpiraEmDias = Convert.ToInt32(configuration["User:TimeDayExpireLoginFornecedor"]);
            var diferenca = DateTime.UtcNow - usuario.UltimoLogin.Value;
            if (diferenca.Days > loginFornecedorExpiraEmDias)
            {
                return new CommandResponse<LoginInicialResponse>(new LoginInicialResponse(usuario.Email), this);
            }
        }

        return new CommandResponse<LoginInicialResponse>(
            new LoginInicialResponse(
                usuario.Id,
                usuario.Nome,
                usuario.Email.MascaraEmail(),
                usuario.Telefone is not null ? usuario.Telefone!.MascaraTelefone() : null),
            this);
    }
}