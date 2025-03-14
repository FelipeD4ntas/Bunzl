using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Interfaces.Email;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Domain.Commands.Fornecedor.Adicionar;
using Bunzl.Domain.Commands.Fornecedor.AdicionarAnexoProduto;
using Bunzl.Domain.Commands.Fornecedor.AdicionarDocumento;
using Bunzl.Domain.Commands.Fornecedor.AdicionarObservacao;
using Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProduto;
using Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProdutoBunzl;
using Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaTabelaPreco;
using Bunzl.Domain.Commands.Fornecedor.AdicionarProduto;
using Bunzl.Domain.Commands.Fornecedor.Ativar;
using Bunzl.Domain.Commands.Fornecedor.Atualizar;
using Bunzl.Domain.Commands.Fornecedor.AtualizarProduto;
using Bunzl.Domain.Commands.Fornecedor.DeletarAnexoProduto;
using Bunzl.Domain.Commands.Fornecedor.DeletarDocumento;
using Bunzl.Domain.Commands.Fornecedor.DeletarObservacao;
using Bunzl.Domain.Commands.Fornecedor.DeletarProduto;
using Bunzl.Domain.Commands.Fornecedor.Inativar;
using Bunzl.Domain.Commands.Fornecedor.InvalidarPortal;
using Bunzl.Domain.Commands.Fornecedor.Listar;
using Bunzl.Domain.Commands.Fornecedor.ListarAnexosProduto;
using Bunzl.Domain.Commands.Fornecedor.ListarDocumentos;
using Bunzl.Domain.Commands.Fornecedor.ListarObservacoes;
using Bunzl.Domain.Commands.Fornecedor.ListarProdutos;
using Bunzl.Domain.Commands.Fornecedor.Obter;
using Bunzl.Domain.Commands.Fornecedor.ObterAnexoProduto;
using Bunzl.Domain.Commands.Fornecedor.ObterDocumento;
using Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaCadastroProduto;
using Bunzl.Domain.Commands.Fornecedor.ObterProduto;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.Data.Context;
using MediatR;
using Bunzl.Domain.Commands.Fornecedor.ListarHomologadoComTabelaPreco;
using Bunzl.Domain.Commands.Fornecedor.ListarProdutosHomologado;
using Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaCadastroProdutoBunzl;
using Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaTabelaPreco;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;

namespace Bunzl.Application.Services;

public class FornecedorAppService(IUnitOfWork unitOfWork, ISender mediator, IEmailService emailService, IUsuarioAutenticado usuarioAutenticado, IRepositoryEmpresa repositoryEmpresa, BunzlContext bunzlContext) : Notifiable, IFornecedorAppService, IInjectScoped
{
    public async Task<CommandResponse<DataSourcePageResponse>> ListarFornecedor(FornecedorListarRequest fornecedorListarDevExpressRequest)
    {
        var commandResponse = await mediator.Send(fornecedorListarDevExpressRequest);
        return commandResponse;
    }

    public async Task<CommandResponse<DataSourcePageResponse>> ListarFornecedorProdutosHomologado(FornecedorProdutoListarHomologadoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<DataSourcePageResponse>> ListarFornecedorHomologadoComTabelaPreco(FornecedorListarHomologadoComTabelaPrecoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<DataSourcePageResponse>> ListarFornecedorDocumentos(FornecedorListarDocumentosRequest fornecedorListarDocumentosDevExpressRequest)
    {
        var commandResponse = await mediator.Send(fornecedorListarDocumentosDevExpressRequest);
        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorAdicionarResponse>> Adicionar(FornecedorAdicionarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        var usuarios = commandResponse.Dados?.Usuarios;
        var fornecedorNome = commandResponse.Dados?.NomeFornecedor;

        if (commandResponse.Sucesso && usuarios is not null && usuarios.Count > 0 && !string.IsNullOrEmpty(fornecedorNome))
        {
            usuarios.ForEach(async usuario =>
            {
                var retEmail = await emailService.EnviarEmailNovoFornecedorCadastrado(usuario.Email, usuario.Nome, fornecedorNome);
                if (retEmail == null || !retEmail.Sucesso)
                {
                    AddNotification("ServiceFornecedor", FornecedorResources.FalhaEnviarEmailNovoFornecedor);
                }
            });
        }

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorAtualizarResponse>> AtualizarFornecedor(FornecedorAtualizarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        var fornecedor = commandResponse.Dados?.Fornecedor;
        var usuario = commandResponse.Dados?.Usuario;
        var usuarios = commandResponse.Dados?.Usuarios;
        var emailParaFornecedor = commandResponse.Dados.EmailParaFornecedor;
        var atualizacaoSimples = commandResponse.Dados.AtualizacaoSimples;

        var empresa = await repositoryEmpresa.GetByAsync(false, p => p.Id == usuarioAutenticado.UsuarioEmpresa);

		if (commandResponse.Sucesso && usuarios is not null && emailParaFornecedor)
        {
            usuarios.ForEach(async usuario =>
            {
                // EMAIL PARA OS USUARIOS DA EMPRES INFORMANDO QUE O FORNECEDOR x PRECISA SER HOMOLOGADO NOVAMENTE
                var retEmail = await emailService.EnviarEmailFornecedorHomologar(usuario.Email, usuario.Nome, fornecedor.NomeFantasia, commandResponse.Dados.Id);
                if (retEmail == null || !retEmail.Sucesso)
                {
                    AddNotification("ServiceFornecedor", FornecedorResources.FalhaEnviarEmailHomologar);
                }
            });
        } 
        else if (commandResponse.Sucesso && usuario is not null && !string.IsNullOrEmpty(usuario.Email) && !emailParaFornecedor && !string.IsNullOrEmpty(usuario.ChaveCadastro.ToString()) && !atualizacaoSimples)
        {
			// EMAIL PARA O USUARIO DO FORNECEDOR IMPORTADO DO ERP, PARA CADASTRAR A SENHA DO SEU USUARIO
			var retEmail = await emailService.EnviarEmailUsuarioCadastro(usuario.Email, usuario.Nome, usuario.ChaveCadastro.Value);
            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceUsuario", UsuarioResources.FalhaEnviarEmailCadastro);
                return new CommandResponse<FornecedorAtualizarResponse>(this);
            }
        }
        else if(commandResponse.Sucesso && !string.IsNullOrEmpty(fornecedor?.Email) && !emailParaFornecedor && !atualizacaoSimples)
        {
			// EMAIL PARA O FORNECEDOR, INFORMANDO QUE ELE FOI HOMOLOGADO
			var retEmail = await emailService.EnviarEmailFornecedorFoiHomologado(fornecedor.Email, fornecedor.NomeFantasia, empresa!.Nome);
            if (retEmail == null || !retEmail.Sucesso)
            {
                AddNotification("ServiceFornecedor", FornecedorResources.FalhaEnviarEmailFoiHomologado);
                return new CommandResponse<FornecedorAtualizarResponse>(this);
            }
        }

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorAdicionarDocumentoResponse>> AdicionarDocumento(FornecedorAdicionarDocumentoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        if (commandResponse is { Sucesso: true, Dados.FornecedorDocumento: not null })
        {
            commandResponse.Dados.DocumentoId = commandResponse.Dados.FornecedorDocumento.Id;
        }

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorDeletarDocumentoResponse>> DeletarDocumento(Guid id, Guid documentoId)
    {
        var request = new FornecedorDeletarDocumentoRequest { Id = id, DocumentoId = documentoId };
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorObterResponse>> Obter(FornecedorObterRequest request)
    {
        var commadResponse = await mediator.Send(request);
        AddNotifications(commadResponse.Notificacoes);

        return commadResponse;
    }

    public async Task<CommandResponse<FornecedorObterDocumentoResponse>> ObterDocumento(FornecedorObterDocumentoRequest request)
    {
        var commadResponse = await mediator.Send(request);
        AddNotifications(commadResponse.Notificacoes);

        return commadResponse;
    }

    public async Task<CommandResponse<FornecedorAdicionarObservacaoResponse>> AdicionarObservacao(FornecedorAdicionarObservacaoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<DataSourcePageResponse>> ListarObservacoes(FornecedorListarObservacoesRequest fornecedorListarDocumentosDevExpressRequest)
    {
        var commandResponse = await mediator.Send(fornecedorListarDocumentosDevExpressRequest);
        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorDeletarObservacaoResponse>> DeletarObservacao(FornecedorDeletarObservacaoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorAtivarResponse>> AtivarFornecedor(Guid id)
    {
        var request = new FornecedorAtivarRequest { Id = id };
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorInativarResponse>> InativarFornecedor(Guid id)
    {
        var request = new FornecedorInativarRequest { Id = id };
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorInvalidarPortalResponse>> InvalidarPortalFornecedor(Guid id)
    {
        var request = new FornecedorInvalidarPortalRequest { Id = id };
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<ProdutosAdicionarPlanilhaResponse>> AdicionarPlanilhaProduto(ProdutosAdicionarPlanilhaRequest request)
    {
		// EMAIL É ENVIADO PARA TODOS OS USUARIOS QUE TENHAM O PERFIL CompradorKeyUser OU AdministradorSuperUser 
		// INFORMANDO QUE UMA PLANILHA DE PRODUTOS FOI IMPORTADA POR UM FORNECEDOR
		var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        if (commandResponse.Sucesso && commandResponse?.Dados?.QuantidadeProdutosImportados != 0 && !String.IsNullOrEmpty(commandResponse?.Dados?.FornecedorNome))
        {
            commandResponse?.Dados?.Usuarios?.ForEach(async usuario =>
            {
                var retEmail = await emailService.EnviarEmailProdutoImportado(
                    usuario.Email, 
                    usuario.Nome, 
                    commandResponse?.Dados?.FornecedorNome, 
                    commandResponse.Dados.QuantidadeProdutosImportados
                );

                if (retEmail == null || !retEmail.Sucesso)
                {
                    AddNotification("ServiceProduto", FornecedorResources.ProdutoFalhaEnviarEmailProdutoALterado);
                }
            });
        }

        return commandResponse;
    }

    public async Task<CommandResponse<ProdutosObterPlanilhaResponse>> ObterPlanilhaProduto()
    {
        var commadResponse = await mediator.Send(new ProdutosObterPlanilhaRequest());
        AddNotifications(commadResponse.Notificacoes);

        return commadResponse;
    }

    public async Task<CommandResponse<ProdutosObterPlanilhaBunzlResponse>> ObterPlanilhaProdutoBunzl(ProdutosObterPlanilhaBunzlRequest request)
    {
        var commadResponse = await mediator.Send(request);
        AddNotifications(commadResponse.Notificacoes);

        return commadResponse;
    }

    public async Task<CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>> AdicionarPlanilhaProdutoBunzl(ProdutosAdicionarPlanilhaBunzlRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<ProdutosObterPlanilhaTabelaPrecoResponse>> ObterPlanilhaTabelaPreco(ProdutosObterPlanilhaTabelaPrecoRequest request)
    {
        var commadResponse = await mediator.Send(request);
        AddNotifications(commadResponse.Notificacoes);

        return commadResponse;
    }

    public async Task<CommandResponse<DataSourcePageResponse>> AdicionarPlanilhaTabelaPreco(ProdutosAdicionarPlanilhaTabelaPrecoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorProdutoAdicionarResponse>> AdicionarProduto(FornecedorProdutoAdicionarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        var usuarios = commandResponse.Dados?.Usuarios;
        var fornecedorNome = commandResponse.Dados?.FornecedorNome;
        var produtoDescricao = commandResponse.Dados?.DescricaoCompletaProduto;

        if (commandResponse.Sucesso && usuarios is not null && usuarios.Count > 0 && !string.IsNullOrEmpty(fornecedorNome) && !string.IsNullOrEmpty(produtoDescricao))
        {
            usuarios.ForEach(async usuario =>
            {
                var retEmail = await emailService.EnviarEmailProdutoAdicionado(usuario.Email, usuario.Nome, fornecedorNome, produtoDescricao);
                if (retEmail == null || !retEmail.Sucesso)
                {
                    AddNotification("ServiceProduto", FornecedorResources.ProdutoFalhaEnviarEmailProdutoALterado);
                }
            });
        }

        if (commandResponse is { Sucesso: true, Dados.FornecedorProduto: not null })
        {
            commandResponse.Dados.ProdutoId = commandResponse.Dados.FornecedorProduto.Id;
        }

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorProdutoAtualizarResponse>> AtualizarProduto(FornecedorProdutoAtualizarRequest request)
    {
		// EMAIL É ENVIADO PARA TODOS OS USUARIOS QUE TENHAM O PERFIL CompradorKeyUser OU AdministradorSuperUser 
		var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        var usuarios = commandResponse.Dados?.Usuarios;
        var fornecedorNome = commandResponse.Dados?.NomeFornecedor;
        var produtoCodigo = commandResponse.Dados?.Sku;
        var produtoDescricao = commandResponse.Dados?.DescricaoCompletaProduto;

        if (commandResponse.Sucesso && usuarios is not null && usuarios.Count > 0 && !string.IsNullOrEmpty(fornecedorNome) && !string.IsNullOrEmpty(produtoCodigo) && !string.IsNullOrEmpty(produtoDescricao))
        {
            usuarios.ForEach(async usuario =>
            {
                var retEmail = await emailService.EnviarEmailProdutoAlterado(usuario.Email, usuario.Nome, fornecedorNome, produtoCodigo, produtoDescricao);
                if (retEmail == null || !retEmail.Sucesso)
                {
                    AddNotification("ServiceProduto", FornecedorResources.ProdutoFalhaEnviarEmailProdutoALterado);
                }
            });
        }

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorProdutoDeletarResponse>> DeletarProduto(FornecedorProdutoDeletarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorProdutoAdicionarAnexoResponse>> AdicionarProdutoAnexo(FornecedorProdutoAdicionarAnexoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        var usuarios = commandResponse.Dados?.Usuarios;
        var fornecedorNome = commandResponse.Dados?.FornecedorNome;
        var produtoCodigo = commandResponse.Dados?.Sku;
        var produtoDescricao = commandResponse.Dados?.DescricaoCompletaProduto;

        if (commandResponse.Sucesso && usuarios is not null && usuarios.Count > 0 && !string.IsNullOrEmpty(fornecedorNome) && !string.IsNullOrEmpty(produtoCodigo) &&!string.IsNullOrEmpty(produtoDescricao))
        {
            usuarios.ForEach(async usuario =>
            {
                var retEmail = await emailService.EnviarEmailNovoAnexoProduto(usuario.Email, usuario.Nome, fornecedorNome, produtoCodigo, produtoDescricao);
                if (retEmail == null || !retEmail.Sucesso)
                {
                    AddNotification("ServiceProduto", FornecedorResources.ProdutoFalhaEnviarEmailProdutoALterado);
                }
            });
        }

        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorProdutoDeletarAnexoResponse>> DeletarProdutoAnexo(FornecedorProdutoDeletarAnexoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }

    public async Task<CommandResponse<DataSourcePageResponse>> ListarFornecedorProdutos(FornecedorProdutoListarRequest fornecedorListarProdutosDevExpressRequest)
    {
        var commandResponse = await mediator.Send(fornecedorListarProdutosDevExpressRequest);
        return commandResponse;
    }

    public async Task<CommandResponse<DataSourcePageResponse>> ListarFornecedorProdutoAnexos(FornecedorProdutoListarAnexoRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<FornecedorProdutoObterAnexoResponse>> ObterFornecedorProdutoAnexo(FornecedorProdutoObterAnexoRequest request)
    {
        var commadResponse = await mediator.Send(request);
        AddNotifications(commadResponse.Notificacoes);

        return commadResponse;
    }

    public async Task<CommandResponse<FornecedorObterProdutoResponse>> ObterProduto(FornecedorObterProdutoRequest request)
    {
        var commadResponse = await mediator.Send(request);
        AddNotifications(commadResponse.Notificacoes);

        return commadResponse;
    }
}