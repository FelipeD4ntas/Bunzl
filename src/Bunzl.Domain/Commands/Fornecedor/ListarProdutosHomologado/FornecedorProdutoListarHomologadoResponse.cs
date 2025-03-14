using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.Fornecedor.ListarProdutosHomologado;

public class FornecedorProdutoListarHomologadoResponse
{
	public IEnumerable<FornecedorProdutoDto>? FornecedorProdutos { get; set; }
}

