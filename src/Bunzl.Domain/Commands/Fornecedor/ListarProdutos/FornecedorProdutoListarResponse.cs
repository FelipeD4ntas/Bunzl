using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.Fornecedor.ListarProdutos;

public class FornecedorProdutoListarResponse
{
    public IEnumerable<FornecedorProdutoDto>? FornecedorProdutos { get; set; }
}
