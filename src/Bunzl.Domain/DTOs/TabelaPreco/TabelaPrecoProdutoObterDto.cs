using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.DTOs.TabelaPreco;

public class TabelaPrecoProdutoObterDto
{
    public Guid Id { get; set; }
    public ProdutoDto Produto { get; set; }
    public decimal UltimoPrecoPraticado { get; set; }
    public decimal NovoPreco { get; set; }
    public EStatusTabelaPrecoProduto Status { get; set; }
}