using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.Interfaces.Email;
using Bunzl.Core.Domain.Interfaces.Sms;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Domain.Commands.Login.LoginAlterarEmpresa;
using Bunzl.Domain.Commands.Login.LoginDev;
using Bunzl.Domain.Commands.Login.LoginFinal;
using Bunzl.Domain.Commands.Login.LoginGerarCodigoOtp;
using Bunzl.Domain.Commands.Login.LoginInicial;
using Bunzl.Domain.Commands.Usuario.SolicitarResetSenha;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Application.Services;

public class LoginAppService(
    IUnitOfWork unitOfWork,
    ISender mediator,
    IEmailService emailService,
    ISmsAzureService smsService,
    IUsuarioAppService usuarioAppService)
    : Notifiable, ILoginAppService, IInjectScoped
{
    public async Task<CommandResponse<LoginDevResponse>> LoginDev()
    {
        var commandResponse = await mediator.Send(new LoginDevRequest());
        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<LoginAlterarEmpresaResponse>> LoginAlterarEmpresa(LoginAlterarEmpresaRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<LoginFinalResponse>> LoginFinal(LoginFinalRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<LoginGerarCodigoOtpResponse>> LoginGerarCodigoEmail(LoginGerarCodigoOtpRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        if (commandResponse.Sucesso && commandResponse.Dados != null)
        {
            var retEmail = await emailService.EnviarEmailUsuarioCodigoOtp(commandResponse.Dados.Email, commandResponse.Dados.Nome, commandResponse.Dados.CodigoOtp);
            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("Usuario", LoginResources.FalhaEnviarEmailLogin);
                return await Task.FromResult(new CommandResponse<LoginGerarCodigoOtpResponse>(this));
            }
        }

        return commandResponse;
    }

    public async Task<CommandResponse<LoginGerarCodigoOtpResponse>> LoginGerarCodigoSms(LoginGerarCodigoOtpRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        if (commandResponse.Sucesso && commandResponse.Dados != null)
        {
            var retSms = await smsService.EnviarSmsCodigoOtp(commandResponse.Dados.Area, commandResponse.Dados.Telefone, commandResponse.Dados.CodigoOtp);
            if (retSms == null || !retSms.Sucesso)
            {
                AddNotification("Usuario", retSms?.Mensagem ?? LoginResources.FalhaEnviarEmailLogin);
                return await Task.FromResult(new CommandResponse<LoginGerarCodigoOtpResponse>(this));
            }
        }

        return commandResponse;
    }

    public async Task<CommandResponse<LoginInicialResponse>> LoginInicial(LoginInicialRequest request)
    {
        var commandResponse = await mediator.Send(request);

        if (commandResponse.Sucesso
            && commandResponse.Dados?.Email != null
            && commandResponse.Dados.UsuarioSenhaExpirada)
        {
            var requestSolicitarResetSenha = new UsuarioSolicitarResetSenhaRequest(commandResponse.Dados.Email);
            await usuarioAppService.SolicitarResetSenha(requestSolicitarResetSenha);

            AddNotification("Usuario", LoginResources.LoginExpirado);
            return new CommandResponse<LoginInicialResponse>(commandResponse.Dados, this);
        }

        return commandResponse;
    }
}