using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Interfaces.Email;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;
using Bunzl.Domain.Commands.TabelaPreco.Adicionar;
using Bunzl.Domain.Commands.TabelaPreco.Atualizar;
using Bunzl.Domain.Commands.TabelaPreco.Listar;
using Bunzl.Domain.Commands.TabelaPreco.ListarProdutos;
using Bunzl.Domain.Commands.TabelaPreco.ObterAguardandoAprovacao;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Commands.TabelaPreco.Aprovar;
using Bunzl.Domain.Commands.TabelaPreco.Cancelar;
using Bunzl.Domain.Commands.TabelaPreco.Validar;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;

namespace Bunzl.Application.Services;

public class TabelaPrecoAppService(
	IUnitOfWork unitOfWork, 
	ISender mediator, 
	IEmailService emailService, 
	IRepositoryEmpresa repositoryEmpresa,
    IUsuarioAutenticado usuarioAutenticado) : Notifiable, ITabelaPrecoAppService, IInjectScoped
{
    public async Task<CommandResponse<DataSourcePageResponse>> Listar(TabelaPrecoListarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<TabelaPrecoObterAguardandoAprovacaoResponse>> ObterAguardandoAprovacao(TabelaPrecoObterAguardandoAprovacaoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<List<TabelaPrecoListarProdutosResponse>>> ObterProdutos(TabelaPrecoListarProdutosRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<TabelaPrecoAdicionarResponse>> Adicionar(TabelaPrecoAdicionarRequest request)
    {
        // EMAIL É ENVIADO PARA TODOS OS USUARIOS QUE TENHAM O PERFIL CompradorKeyUser OU AdministradorSuperUser 
        // INFORMANDO QUE A TABELA DE PREÇO FOI IMPORTADA PELO FORNECEDOR X
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

		var usuarios = commandResponse.Dados?.Usuarios;
		var fornecedorNome = commandResponse.Dados?.NomeFornecedor;

		if (IsValid())
            await unitOfWork.CommitAsync();

		if (commandResponse.Sucesso && usuarios is not null && usuarios.Count > 0 && !string.IsNullOrEmpty(fornecedorNome))
		{
			usuarios.ForEach(async usuario =>
			{
                var retEmail = await emailService.EnviarEmailNotificaUsuarioTabelaImportada(
                    usuario.Email,
                    usuario.Nome,
					fornecedorNome
				);

                if (retEmail == null || !retEmail.Sucesso)
				{
					AddNotification("ServiceFornecedor", TabelaPrecoResources.FalhaEnviarEmailTabelaPrecoImportada);
				}
			});
		}

		return commandResponse;
    }

    public async Task<CommandResponse<TabelaPrecoAtualizarResponse>> Atualizar(TabelaPrecoAtualizarRequest request)
    {
        // EMAIL É ENVIADO PARA TODOS OS USUARIOS QUE TENHAM O PERFIL CompradorKeyUser OU AdministradorSuperUser 
        // INFORMANDO QUE A TABELA DE PREÇO FOI ATUALIZADA PELO FORNECEDOR X
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

		var usuarios = commandResponse.Dados?.Usuarios;
		var fornecedorNome = commandResponse.Dados?.NomeFornecedor;

		if (IsValid())
            await unitOfWork.CommitAsync();

		if (commandResponse.Sucesso && usuarios is not null && usuarios.Count > 0 && !string.IsNullOrEmpty(fornecedorNome))
		{
			usuarios.ForEach(async usuario =>
			{
				var retEmail = await emailService.EnviarEmailNotificaUsuarioTabelaAtualizada(
					usuario.Email,
					usuario.Nome,
					fornecedorNome
				);

				if (retEmail == null || !retEmail.Sucesso)
				{
					AddNotification("ServiceFornecedor", TabelaPrecoResources.FalhaEnviarEmailTabelaPrecoAtualizada);
				}
			});
		}

		return commandResponse;
    }

    public async Task<CommandResponse<TabelaPrecoValidarResponse>> Validar(TabelaPrecoValidarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        var fornecedor = commandResponse.Dados?.Fornecedor;
        var tabelaValidada = commandResponse.Dados?.TabelaValidada;

        if (IsValid())
            await unitOfWork.CommitAsync();


        if (commandResponse.Sucesso && fornecedor is not null && tabelaValidada == false)
        {
            var empresa = await repositoryEmpresa.GetByAsync(false, p => p.Id == usuarioAutenticado.UsuarioEmpresa);

            var retEmail = await emailService.EnviarEmailNotificaUsuarioTabelaReprovada(
                   fornecedor.Email,
                   fornecedor.NomeFantasia,
                   usuarioAutenticado.UsuarioNome,
                   empresa!.Nome
               );

            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceFornecedor", TabelaPrecoResources.FalhaEnviarEmailTabelaPrecoAprovada);
            }
        }
        else if (commandResponse.Sucesso && fornecedor is not null && tabelaValidada == true)
        {
            var empresa = await repositoryEmpresa.GetByAsync(false, p => p.Id == usuarioAutenticado.UsuarioEmpresa);

            var retEmail = await emailService.EnviarEmailNotificaUsuarioTabelaAprovada(
                   fornecedor.Email,
                   fornecedor.NomeFantasia,
                   usuarioAutenticado.UsuarioNome,
                   empresa!.Nome
               );

            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceFornecedor", TabelaPrecoResources.FalhaEnviarEmailTabelaPrecoAprovada);
            }
        }

        return commandResponse;
    }

    public async Task<CommandResponse<TabelaPrecoAprovarResponse>> Aprovar(TabelaPrecoAprovarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        var fornecedor = commandResponse.Dados?.Fornecedor;

        if (IsValid())
            await unitOfWork.CommitAsync();

        if (commandResponse.Sucesso && fornecedor is not null)
        {
            var empresa = await repositoryEmpresa.GetByAsync(false, p => p.Id == usuarioAutenticado.UsuarioEmpresa);

            var retEmail = await emailService.EnviarEmailNotificaUsuarioTabelaAprovada(
                   fornecedor.Email,
                   fornecedor.NomeFantasia,
                   usuarioAutenticado.UsuarioNome, 
                   empresa!.Nome
               );

            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceFornecedor", TabelaPrecoResources.FalhaEnviarEmailTabelaPrecoAprovada);
            }
        }

        return commandResponse;
    }

    public async Task<CommandResponse<TabelaPrecoCancelarResponse>> Cancelar(TabelaPrecoCancelarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }
}