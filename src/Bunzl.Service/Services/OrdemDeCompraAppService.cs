using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.Interfaces.Email;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.Data.Context;
using MediatR;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Commands.OrdemDeCompra.Adicionar;
using Bunzl.Domain.Commands.OrdemDeCompra.AdicionarAnexo;
using Bunzl.Domain.Commands.OrdemDeCompra.AdicionarObservacao;
using Bunzl.Domain.Commands.OrdemDeCompra.AtualizarStatus;
using Bunzl.Domain.Commands.OrdemDeCompra.DeletarAnexo;
using Bunzl.Domain.Commands.OrdemDeCompra.DeletarObservacao;
using Bunzl.Domain.Commands.OrdemDeCompra.Listar;
using Bunzl.Domain.Commands.OrdemDeCompra.ListarAnexos;
using Bunzl.Domain.Commands.OrdemDeCompra.Obter;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Domain.Commands.OrdemDeCompra.ListarObservacoes;
using Bunzl.Domain.Commands.OrdemDeCompra.ObterAnexo;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Domain.Entities;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;

namespace Bunzl.Application.Services;

public class OrdemDeCompraAppService(IUnitOfWork unitOfWork, ISender mediator, IEmailService emailService, IUsuarioAutenticado usuarioAutenticado, IRepositoryEmpresa repositoryEmpresa) : Notifiable, IOrdemDeCompraAppService, IInjectScoped
{
	public async Task<CommandResponse<OrdemDeCompraAdicionarResponse>> Adicionar(OrdemDeCompraAdicionarRequest request)
	{
		var commandResponse = await mediator.Send(request);
		AddNotifications(commandResponse.Notificacoes);

		if (IsValid())
			await unitOfWork.CommitAsync();

        return commandResponse;
	}

	public async Task<CommandResponse<DataSourcePageResponse>> Listar(OrdemDeCompraListarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<OrdemDeCompraObterResponse>> Obter(OrdemDeCompraObterRequest request)
    {
        var commadResponse = await mediator.Send(request);
        AddNotifications(commadResponse.Notificacoes);

        return commadResponse;
    }

    public async Task<CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>> AdicionarObservacao(OrdemDeCompraAdicionarObservacaoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        var empresa = commandResponse.Dados?.Empresa;
        var fornecedor = commandResponse.Dados?.Fornecedor;
        var usuariosTimeBunzl = commandResponse.Dados?.Usuarios;
        var numeroOrdemDeCompra = commandResponse.Dados?.Numero;
        var usuarioAutenticadoNome = commandResponse.Dados?.UsuarioAutenticadoNome;

        if (commandResponse.Sucesso && fornecedor is not null && empresa is not null && usuariosTimeBunzl is not null && !String.IsNullOrEmpty(numeroOrdemDeCompra))
        {
            commandResponse?.Dados?.Usuarios?.ForEach(async usuario =>
            {
                var retEmail = await emailService.EnviarEmailNovaObservacaoOrdemDeCompraTimeBunzl(
                    usuario.Email,
                    fornecedor.NomeFantasia,
                    usuario.Nome,
                    numeroOrdemDeCompra,
                    empresa.Nome
                );

                if (retEmail == null || !retEmail.Sucesso)
                {
                    AddNotification("ServiceNegociacaoComercial", OrdemDeCompraResources.FalhaEnviarEmailNovaObservacao);
                }
            });
        }
        else if (commandResponse.Sucesso && fornecedor is not null && empresa is not null && usuariosTimeBunzl is null && !String.IsNullOrEmpty(numeroOrdemDeCompra) && !String.IsNullOrEmpty(usuarioAutenticadoNome))
        {
            var retEmail = await emailService.EnviarEmailNovaObservacaoOrdemDeCompraParaFornecedor(
                fornecedor.Email,
                fornecedor.NomeFantasia,
                usuarioAutenticadoNome,
                numeroOrdemDeCompra,
                empresa.Nome
			);

            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceNegociacaoComercial", OrdemDeCompraResources.FalhaEnviarEmailNovaObservacao);
            }
        }

        return commandResponse;
    }

    public async Task<CommandResponse<DataSourcePageResponse>> ListarObservacoes(OrdemDeCompraListarObservacoesRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<OrdemDeCompraDeletarObservacaoResponse>> DeletarObservacao(OrdemDeCompraDeletarObservacaoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<OrdemDeCompraAdicionarAnexoResponse>> AdicionarAnexo(OrdemDeCompraAdicionarAnexoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        if (commandResponse is { Sucesso: true, Dados.Anexo: not null })
            commandResponse.Dados.AnexoId = commandResponse.Dados.Anexo.Id;

        var empresa = commandResponse.Dados?.Empresa;
        var fornecedor = commandResponse.Dados?.Fornecedor;
        var usuarioAutenticadoNome = commandResponse.Dados?.UsuarioAutenticadoNome;
        var usuarios = commandResponse.Dados?.Usuarios;
		var numeroOrdemDeCompra = commandResponse.Dados?.Numero;
        var ehPerfilFornecedor = commandResponse.Dados?.EhPerfilFornecedor ?? true;
        var status = commandResponse.Dados?.Status;

		if (commandResponse.Sucesso && fornecedor is not null && empresa is not null && ehPerfilFornecedor)
        {
            usuarios!.ForEach(async usuario =>
			{
				var retEmail = await emailService.EnviarEmailNovaAnexoOrdemDeCompraFornecedor(
					usuario.Email,
					fornecedor.NomeFantasia,
					usuario.Nome,
					numeroOrdemDeCompra,
					empresa.Nome,
					status!
				);

				if (retEmail == null || !retEmail.Sucesso)
					AddNotification("ServiceNegociacaoComercial", OrdemDeCompraResources.FalhaEnviarEmailNovaObservacao);
			});
        }

        return commandResponse;
    }

    public async Task<CommandResponse<OrdemDeCompraDeletarAnexoResponse>> DeletarAnexo(OrdemDeCompraDeletarAnexoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<DataSourcePageResponse>> ListarAnexos(OrdemDeCompraListarAnexosRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<OrdemDeCompraObterAnexoResponse>> ObterAnexoOrdemDeCompra(OrdemDeCompraObterAnexoRequest request)
    {
        var commadResponse = await mediator.Send(request);
        AddNotifications(commadResponse.Notificacoes);

        return commadResponse;
    }

    public async Task<CommandResponse<OrdemDeCompraAtualizarStatusResponse>> AtualizarStatus(OrdemDeCompraAtualizarStatusRequest request)
    {
        // EMAIL É ENVIADO PARA O FORNECEDOR AVISANDO QUE O STATUS DA ORDEM DE COMPRA FOI ALTERADO
        var commandResponse = await mediator.Send(request);
		AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        var empresa = await repositoryEmpresa.GetByAsync(false, e => e.Id == usuarioAutenticado.UsuarioEmpresa);

        var fornecedor = commandResponse.Dados?.Fornecedor;
		var status = commandResponse.Dados?.Status;
		var numeroOrdemDeCompra = commandResponse.Dados?.NumeroOrdemDeCompra;

        if (commandResponse.Sucesso && fornecedor is not null)
        {
            if (status is "Em Produção" or "In Production")
            {
                var retEmail = await emailService.EnviarEmailNovaAnexoOrdemDeCompraTimeBunzl(
                    fornecedor.Email,
                    fornecedor.NomeFantasia,
                    usuarioAutenticado.UsuarioNome,
                    numeroOrdemDeCompra,
                    empresa!.Nome,
                    status.ToString()
                );

                if (retEmail is not { Sucesso: true })
                {
                    AddNotification("ServiceOrdemDeCompra", OrdemDeCompraResources.FalhaEnviarEmailStatusAlterado);
                }
            }
            else
            {
                var retEmail = await emailService.EnviarEmailNovoStatusOrdemDeCompra(
                    fornecedor.Email,
                    fornecedor.NomeFantasia,
                    numeroOrdemDeCompra,
                    status
                );

                if (retEmail is not { Sucesso: true })
                {
                    AddNotification("ServiceOrdemDeCompra", OrdemDeCompraResources.FalhaEnviarEmailStatusAlterado);
                }
            }
        }

		return commandResponse;
	}
}

