using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Interfaces.Email;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Domain.Commands.Usuario.Adicionar;
using Bunzl.Domain.Commands.Usuario.Ativar;
using Bunzl.Domain.Commands.Usuario.Atualizar;
using Bunzl.Domain.Commands.Usuario.AtualizarSenhaTelefone;
using Bunzl.Domain.Commands.Usuario.ConfirmarCadastro;
using Bunzl.Domain.Commands.Usuario.Deletar;
using Bunzl.Domain.Commands.Usuario.Inativar;
using Bunzl.Domain.Commands.Usuario.Listar;
using Bunzl.Domain.Commands.Usuario.Obter;
using Bunzl.Domain.Commands.Usuario.ResetSenha;
using Bunzl.Domain.Commands.Usuario.SolicitarResetSenha;
using Bunzl.Domain.Commands.Usuario.VerificarResetSenha;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.Data.Context;
using MediatR;

namespace Bunzl.Application.Services;

public class UsuarioAppService(IUnitOfWork unitOfWork, ISender mediator, IEmailService emailService, BunzlContext context)
    : Notifiable, IUsuarioAppService, IInjectScoped
{
    public async Task<CommandResponse<DataSourcePageResponse>> ListarUsuario(UsuarioListarRequest usuarioListarDevExpressRequest)
    {
        var commandResponse = await mediator.Send(usuarioListarDevExpressRequest);
        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioObterResponse>> ObterUsuario(UsuarioObterRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioAdicionarResponse>> Adicionar(UsuarioAdicionarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        if (commandResponse.Sucesso && commandResponse.Dados?.Email != null && commandResponse.Dados.JaFoiHomologado && commandResponse.Dados.EmpresaSolicitante != null)
        {
            var retEmail = await emailService.EnviarEmailUsuarioJaCadastrado(commandResponse.Dados!.Email, commandResponse.Dados.Nome, commandResponse.Dados.EmpresaSolicitante);
            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceUsuario", UsuarioResources.FalhaEnviarEmailUsuarioJaCadastrado);
                return new CommandResponse<UsuarioAdicionarResponse>(this);
            }
        } 
        else if (commandResponse.Sucesso && commandResponse.Dados?.Email != null && !commandResponse.Dados.JaFoiHomologado)
        {
            var retEmail = await emailService.EnviarEmailUsuarioCadastro(commandResponse.Dados!.Email, commandResponse.Dados.Nome, commandResponse.Dados.ChaveCadastro);
            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceUsuario", UsuarioResources.FalhaEnviarEmailCadastro);
                return new CommandResponse<UsuarioAdicionarResponse>(this);
            }
        }

        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioAtualizarResponse>> AtualizarUsuario(UsuarioAtualizarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        if (commandResponse.Sucesso && commandResponse.Dados?.Email != null && commandResponse.Dados?.ChaveCadastro != null)
        {
            var retEmail = await emailService.EnviarEmailUsuarioCadastro(commandResponse.Dados!.Email, commandResponse.Dados.Nome, commandResponse.Dados.ChaveCadastro.Value);
            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceUsuario", UsuarioResources.FalhaEnviarEmailCadastro);
                return new CommandResponse<UsuarioAtualizarResponse>(this);
            }
        }

        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioAtivarResponse>> AtivarUsuario(Guid id)
    {
        var request = new UsuarioAtivarRequest { Id = id };
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioInativarResponse>> InativarUsuario(Guid id)
    {
        var request = new UsuarioInativarRequest { Id = id };
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioDeletarResponse>> DeletarUsuario(Guid id)
    {
        var request = new UsuarioDeletarRequest { Id = id };
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioAtualizarSenhaTelefoneResponse>> AtualizarSenhaTelefone(UsuarioAtualizarSenhaTelefoneRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioConfirmarCadastroResponse>> ConfirmarCadastro(UsuarioConfirmarCadastroRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioSolicitarResetSenhaResponse>> SolicitarResetSenha(UsuarioSolicitarResetSenhaRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        if (commandResponse.Sucesso && commandResponse.Dados?.Email != null)
        {
            var retEmail = await emailService.EnviarEmailUsuarioResetSenha(commandResponse.Dados!.Email, commandResponse.Dados.Nome, commandResponse.Dados.ChaveResetSenha);
            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceUsuario", UsuarioResources.FalhaEnviarEmailResetSenha);
                return new CommandResponse<UsuarioSolicitarResetSenhaResponse>(this);
            }
        }

        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioResetSenhaResponse>> ResetSenha(UsuarioResetSenhaRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<UsuarioVerificarResetSenhaResponse>> VerificarResetSenha(UsuarioVerificarResetSenhaRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }
}