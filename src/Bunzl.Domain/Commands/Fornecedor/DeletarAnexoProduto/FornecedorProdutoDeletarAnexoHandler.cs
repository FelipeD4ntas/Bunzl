using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarAnexoProduto;

public class FornecedorProdutoDeletarAnexoHandler(IPublisher mediator, IRepositoryFornecedor repositoryFornecedor)
	: Notifiable, IRequestHandler<FornecedorProdutoDeletarAnexoRequest, CommandResponse<FornecedorProdutoDeletarAnexoResponse>>
{
	public async Task<CommandResponse<FornecedorProdutoDeletarAnexoResponse>> Handle(FornecedorProdutoDeletarAnexoRequest request, CancellationToken cancellationToken)
	{
		var fornecedor = await repositoryFornecedor.GetByAsync(
			true,
			f => f.Id == request.FornecedorId,
			cancellationToken,
			"FornecedorProdutos", "FornecedorProdutos.FornecedorProdutoAnexos"
		);

		if (fornecedor == null)
		{
			AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
			return new CommandResponse<FornecedorProdutoDeletarAnexoResponse>(this);
		}

		var produto = fornecedor.FornecedorProdutos
			.FirstOrDefault(p => p.Id == request.FornecedorProdutoId);

		if (produto == null)
		{
			AddNotification("Produto", FornecedorResources.ProdutoNaoEncontrado);
			return new CommandResponse<FornecedorProdutoDeletarAnexoResponse>(this);
		}

		var anexo = produto.FornecedorProdutoAnexos
			.FirstOrDefault(a => a.Id == request.AnexoId);

		if (anexo == null)
		{
			AddNotification("Anexo", FornecedorResources.AnexoNaoEncontrado);
			return new CommandResponse<FornecedorProdutoDeletarAnexoResponse>(this);
		}

		produto.DeletarAnexo(anexo);

		if (IsInvalid())
			return new CommandResponse<FornecedorProdutoDeletarAnexoResponse>(this);

		repositoryFornecedor.Update(fornecedor);
		await mediator.Publish(new AuditoriaAdicionarInput(fornecedor.Id, TabelasResources.FornecedorProdutoAnexo, "Anexo Deletado", ETipoAuditoria.Modificado));

		var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", produto.Id.ToString());
		var caminhoArquivo = Path.Combine(diretorioArquivo, anexo.Nome);

		if (File.Exists(caminhoArquivo))
		{
			File.Delete(caminhoArquivo);
		}

		var response = new FornecedorProdutoDeletarAnexoResponse(anexo.Id, FornecedorResources.AnexoDeletadoComSucesso);
		return new CommandResponse<FornecedorProdutoDeletarAnexoResponse>(response, this);
	}
}
