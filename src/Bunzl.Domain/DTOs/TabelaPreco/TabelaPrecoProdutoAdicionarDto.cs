namespace Bunzl.Domain.DTOs.TabelaPreco;

public class TabelaPrecoProdutoAdicionarDto
{
    public Guid ProdutoId { get; set; }
    public decimal UltimoPrecoPraticado { get; set; } = decimal.Zero;
    public decimal NovoPreco { get; set; } = decimal.Zero;
}