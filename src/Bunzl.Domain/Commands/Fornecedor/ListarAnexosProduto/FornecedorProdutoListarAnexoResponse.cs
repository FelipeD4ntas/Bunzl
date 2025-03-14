using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.Fornecedor.ListarAnexosProduto;

public class FornecedorProdutoListarAnexoResponse
{
    public IEnumerable<FornecedorProdutoAnexoDto>? FornecedorProdutoAnexos { get; set; }
}
