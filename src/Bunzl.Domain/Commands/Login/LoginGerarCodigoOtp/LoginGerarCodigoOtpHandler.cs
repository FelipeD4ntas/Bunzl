using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.Helper;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Login.LoginGerarCodigoOtp;

public class LoginGerarCodigoOtpHandler(IRepositoryUsuario repositoryUsuario) : Notifiable, IRequestHandler<LoginGerarCodigoOtpRequest, CommandResponse<LoginGerarCodigoOtpResponse>>
{
    public async Task<CommandResponse<LoginGerarCodigoOtpResponse>> Handle(LoginGerarCodigoOtpRequest request, CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(true, p => p.Id == request.Id!, cancellationToken, p => p.Empresas);
        if (usuario == null)
        {
            AddNotification("Usuario", LoginResources.LoginNaoIdentificado);
            return await Task.FromResult(new CommandResponse<LoginGerarCodigoOtpResponse>(this));
        }

        if (!usuario.FlagAtivo)
        {
            AddNotification("Usuário", LoginResources.LoginUsuarioInvativo);
            return await Task.FromResult(new CommandResponse<LoginGerarCodigoOtpResponse>(this));
        }

        if (usuario.Senha == null)
        {
            AddNotification("Usuario", LoginResources.LoginCadastroNaoFinalizado);
            return await Task.FromResult(new CommandResponse<LoginGerarCodigoOtpResponse>(this));
        }

        if (usuario.Empresas == null || !usuario.Empresas.Any())
        {
            AddNotification("Usuário", LoginResources.LoginUsuarioSemEmpresa);
            return await Task.FromResult(new CommandResponse<LoginGerarCodigoOtpResponse>(this));
        }

        string codigoOtp = OtpHelper.GerarCodigoOtp();
        usuario.CodigoOtp = codigoOtp.EncryptPassword();
        usuario.DataGeracaoCodigoOtp = DateTime.UtcNow;

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<LoginGerarCodigoOtpResponse>(this));

        repositoryUsuario.Update(usuario);

        return new CommandResponse<LoginGerarCodigoOtpResponse>(
            new LoginGerarCodigoOtpResponse(
                usuario.Id,
                LoginResources.LoginCodigoGerado,
                usuario.Nome,
                usuario.Email,
                usuario.Area!,
                usuario.Telefone!,
                codigoOtp),
            this);
    }
}