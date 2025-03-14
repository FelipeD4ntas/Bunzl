using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.Interfaces.Email;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Domain.Commands.SignUp;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Application.Services;

public class SignUpAppService(
    IUnitOfWork unitOfWork,
    ISender mediator,
    IEmailService emailService)
    : Notifiable, ISignUpAppService, IInjectScoped
{
    public async Task<CommandResponse<SignUpResponse>> SignUp(SignUpRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        if (commandResponse.Sucesso && commandResponse.Dados?.Email != null)
        { 
            var retEmail = await emailService.EnviarEmailUsuarioCadastro(commandResponse.Dados!.Email, commandResponse.Dados.Nome, commandResponse.Dados.ChaveCadastro);
            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceUsuario",  UsuarioResources.FalhaEnviarEmailCadastro);
                return new CommandResponse<SignUpResponse>(this);
            }
        }

        return commandResponse;
    }
}