using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.Interfaces.Email;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Domain.Commands.NegociacaoComercial.Adicionar;
using Bunzl.Domain.Commands.NegociacaoComercial.AdicionarAnexo;
using Bunzl.Domain.Commands.NegociacaoComercial.AdicionarObservacao;
using Bunzl.Domain.Commands.NegociacaoComercial.DeletarAnexo;
using Bunzl.Domain.Commands.NegociacaoComercial.DeletarObservacao;
using Bunzl.Domain.Commands.NegociacaoComercial.Listar;
using Bunzl.Domain.Commands.NegociacaoComercial.ListarObservacoes;
using Bunzl.Domain.Commands.NegociacaoComercial.Obter;
using Bunzl.Domain.Commands.NegociacaoComercial.ObterAnexoNegociacaoComercial;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Domain.Commands.NegociacaoComercial.Atualizar;
using Bunzl.Domain.Commands.NegociacaoComercial.AtualizarStatus;
using Bunzl.Domain.Commands.NegociacaoComercial.ListarAnexos;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;

namespace Bunzl.Application.Services;

public class NegociacaoComercialAppService(
	IUnitOfWork unitOfWork, 
	ISender mediator, 
	IEmailService emailService,
	IRepositoryTabelaPreco repositoryTabelaPreco,
	IRepositoryEmpresa repositoryEmpresa,
	IRepositoryFornecedor repositoryFornecedor,
	IRepositoryNegociacaoComercial repositoryNegociacaoComercial,
	IUsuarioAutenticado usuarioAutenticado,
	IExternalServiceTabelaPreco externalServiceTabelaPreco) : Notifiable, INegociacaoComercialAppService, IInjectScoped
{
    public async Task<CommandResponse<NegociacaoComercialAdicionarResponse>> Adicionar(NegociacaoComercialAdicionarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        var fornecedor = commandResponse.Dados?.Fornecedor;
        var empresaNome = commandResponse.Dados?.NomeEmpresa;

        if (commandResponse.Sucesso && fornecedor is not null && !string.IsNullOrEmpty(empresaNome))
        {
            var retEmail = await emailService.EnviarEmailSolicitacaoNegociacao(fornecedor.Email, fornecedor.NomeFantasia, empresaNome);
            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceNegociacaoComercial", NegociacaoComercialResources.FalhaEnviarEmailSolicitacaoNegociacao);
            }
        }

        return commandResponse;
    }

    public async Task<CommandResponse<NegociacaoComercialAtualizarResponse>> Atualizar(NegociacaoComercialAtualizarRequest request)
    {
		var commandResponse = await mediator.Send(request);
		AddNotifications(commandResponse.Notificacoes);

		if (IsValid())
			await unitOfWork.CommitAsync();

		return commandResponse;
	}

    public async Task<CommandResponse<NegociacaoComercialAtualizarStatusResponse>> AtualizarStatus(NegociacaoComercialAtualizarStatusRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<DataSourcePageResponse>> ListarNegociacaoComercial(NegociacaoComercialListarRequest request)
    {
		var commandResponse = await mediator.Send(request);
		return commandResponse;
	}

    public async Task<CommandResponse<NegociacaoComercialObterResponse>> Obter(NegociacaoComercialObterRequest request)
    {
		var commadResponse = await mediator.Send(request);
		AddNotifications(commadResponse.Notificacoes);

		return commadResponse;
	}

    public async Task<CommandResponse<NegociacaoComercialAdicionarAnexoResponse>> AdicionarAnexo(NegociacaoComercialAdicionarAnexoRequest request)
    {
		var commandResponse = await mediator.Send(request);
		AddNotifications(commandResponse.Notificacoes);

		if (IsValid())
			await unitOfWork.CommitAsync();

		if (commandResponse is { Sucesso: true, Dados.Anexo: not null })
		{
			commandResponse.Dados.AnexoId = commandResponse.Dados.Anexo.Id;
		}

		return commandResponse;
	}

    public async Task<CommandResponse<DataSourcePageResponse>> ListarAnexos(NegociacaoComercialListarAnexosRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<NegociacaoComercialObterAnexoResponse>> ObterAnexoNegociacaoComercial(NegociacaoComercialObterAnexoRequest request)
    {
		var commadResponse = await mediator.Send(request);
		AddNotifications(commadResponse.Notificacoes);

		return commadResponse;
	}

    public async Task<CommandResponse<NegociacaoComercialDeletarAnexoResponse>> DeletarAnexo(NegociacaoComercialDeletarAnexoRequest request)
    {
		var commandResponse = await mediator.Send(request);
		AddNotifications(commandResponse.Notificacoes);

		if (IsValid())
			await unitOfWork.CommitAsync();

		return commandResponse;
	}

    public async Task<CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>> AdicionarObservacao(NegociacaoComercialAdicionarObservacaoRequest request)
    {
		var commandResponse = await mediator.Send(request);
		AddNotifications(commandResponse.Notificacoes);

		if (IsValid())
			await unitOfWork.CommitAsync();

		var fornecedor = commandResponse.Dados?.Fornecedor;
		var usuariosTimeBunzl = commandResponse.Dados?.Usuarios;
		var codigoNegociacao = commandResponse.Dados?.Codigo;
		var usuarioAutenticadoNome = commandResponse.Dados?.UsuarioAutenticadoNome;

		if (commandResponse.Sucesso && fornecedor is not null && usuariosTimeBunzl is not null && !String.IsNullOrEmpty(codigoNegociacao))
		{
			commandResponse?.Dados?.Usuarios?.ForEach(async usuario =>
			{
				var retEmail = await emailService.EnviarEmailNovaObservacaoNegociacaoTimeBunzl(
					usuario.Email,
					fornecedor.NomeFantasia,
					usuario.Nome,
                    codigoNegociacao
				);

				if (retEmail == null || !retEmail.Sucesso)
				{
					AddNotification("ServiceNegociacaoComercial", NegociacaoComercialResources.FalhaEnviarEmailNovaObservacao);
				}
			});
		} 
		else if (commandResponse.Sucesso && fornecedor is not null && usuariosTimeBunzl is null && !String.IsNullOrEmpty(codigoNegociacao) && !String.IsNullOrEmpty(usuarioAutenticadoNome))
        {
            var retEmail = await emailService.EnviarEmailNovaObservacaoNegociacaoParaFornecedor(
                fornecedor.Email,
                fornecedor.NomeFantasia,
                usuarioAutenticadoNome,
                codigoNegociacao
            );

            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceNegociacaoComercial", NegociacaoComercialResources.FalhaEnviarEmailNovaObservacao);
            }
        }

		return commandResponse;
	}

    public async Task<CommandResponse<NegociacaoComercialDeletarObservacaoResponse>> DeletarObservacao(NegociacaoComercialDeletarObservacaoRequest request)
    {
		var commandResponse = await mediator.Send(request);
		AddNotifications(commandResponse.Notificacoes);

		if (IsValid())
			await unitOfWork.CommitAsync();

		return commandResponse;
	}

    public async Task<CommandResponse<DataSourcePageResponse>> ListarObservacoes(NegociacaoComercialListarObservacoesRequest request)
    {
		var commandResponse = await mediator.Send(request);
		return commandResponse;
	}
}

