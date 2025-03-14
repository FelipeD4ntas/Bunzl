using Bunzl.Domain.Entities;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarAnexoProduto;

public class FornecedorProdutoAdicionarAnexoHandler(IPublisher mediator, 
	IRepositoryFornecedor repositoryFornecedor,
    IRepositoryUsuario repositoryUsuario,
    IUsuarioAutenticado usuarioAutenticado)
	: Notifiable, IRequestHandler<FornecedorProdutoAdicionarAnexoRequest, CommandResponse<FornecedorProdutoAdicionarAnexoResponse>>
{
	public async Task<CommandResponse<FornecedorProdutoAdicionarAnexoResponse>> Handle(FornecedorProdutoAdicionarAnexoRequest request, CancellationToken cancellationToken)
	{
		var tamanhoMaximoMB = 25 * 1024 * 1024;
		if (request.Arquivo.Length > tamanhoMaximoMB)
		{
			AddNotification("Arquivo", FornecedorResources.DocumentoTamanhoMaximo);
			return new CommandResponse<FornecedorProdutoAdicionarAnexoResponse>(this);
		}

		var fornecedor = await repositoryFornecedor.GetByAsync(
			true,
			f => f.Id == request.FornecedorId,
			cancellationToken,
			"FornecedorProdutos", "FornecedorProdutos.FornecedorProdutoAnexos"
		);
		if (fornecedor == null)
		{
			AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
			return new CommandResponse<FornecedorProdutoAdicionarAnexoResponse>(this);
		}

        var usuarios = await repositoryUsuario.ListAsync(true, u => u.PerfilPermissao == EPerfilUsuario.CompradorKeyUser || u.PerfilPermissao == EPerfilUsuario.AdministradorSuperUser, u => u.Empresas);
        var usuariosAutenticados = usuarios
             .Where(u => u.Empresas.Any(e => e.Id == usuarioAutenticado.UsuarioEmpresa))
             .ToList();

        var produto = fornecedor.FornecedorProdutos
			.FirstOrDefault(p => p.Id == request.FornecedorProdutoId);

		if (produto == null)
		{
			AddNotification("Produto", FornecedorResources.ProdutoNaoEncontrado);
			return new CommandResponse<FornecedorProdutoAdicionarAnexoResponse>(this);
		}

		var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fornecedor.Id.ToString(), produto.Id.ToString());
		if (!Directory.Exists(diretorioArquivo))
			Directory.CreateDirectory(diretorioArquivo);

		var caminhoArquivo = Path.Combine(diretorioArquivo, request.Arquivo.FileName);

		var anexoExistente = produto.FornecedorProdutoAnexos
			.FirstOrDefault(a => a.Nome == request.Arquivo.FileName);

		if (anexoExistente is not null)
		{
			await using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
			{
				await request.Arquivo.CopyToAsync(stream, cancellationToken);
			}

			AddNotification("Arquivo", FornecedorResources.ArquivoJaExiste);
			return new CommandResponse<FornecedorProdutoAdicionarAnexoResponse>(this);
		}
		else
		{
			await using (var stream = new FileStream(caminhoArquivo, FileMode.CreateNew))
			{
				await request.Arquivo.CopyToAsync(stream, cancellationToken);
			}

			var anexo = new Entities.FornecedorProdutoAnexo
			{
				Nome = request.Arquivo.FileName,
				Tipo = request.Arquivo.ContentType,
				TipoDocumento = request.TipoDocumento,
				Observacao = request.Observacao,
				FornecedorProdutoId = produto.Id,
				FornecedorProduto = produto
			};

			produto.AdicionarAnexo(anexo);
		}


		repositoryFornecedor.Update(fornecedor);

		await mediator.Publish(new AuditoriaAdicionarInput(produto.Id, TabelasResources.FornecedorProduto, "Anexo Adicionado", ETipoAuditoria.Modificado));

		return new CommandResponse<FornecedorProdutoAdicionarAnexoResponse>(
			new FornecedorProdutoAdicionarAnexoResponse(
				produto.Id, 
				FornecedorResources.AnexoAdicionadoComSuecesso, 
				fornecedor.NomeFantasia, 
				usuariosAutenticados,
				produto.CodigoSku,
                produto.DescricaoCompletaFornecedor),
			this);
	}
}
