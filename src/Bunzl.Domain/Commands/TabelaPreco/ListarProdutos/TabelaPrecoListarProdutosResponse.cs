using Bunzl.Domain.DTOs.TabelaPreco;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.TabelaPreco.ListarProdutos;

public class TabelaPrecoListarProdutosResponse
{
    public Guid Id { get; set; }
    public ProdutoDto Produto { get; set; }
    public decimal UltimoPrecoPraticado { get; set; }
    public decimal NovoPreco { get; set; }
    public EStatusTabelaPrecoProduto Status { get; set; }
}