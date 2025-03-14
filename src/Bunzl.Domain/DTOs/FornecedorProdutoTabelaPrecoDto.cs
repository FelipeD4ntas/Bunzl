namespace Bunzl.Domain.DTOs;

public class FornecedorProdutoTabelaPrecoDto
{
    public Guid ProdutoId { get; set; }
    public string CodigoSku { get; set; }
    public string DescricaoProdutoBunzl { get; set; }
    public string CodigoProdutoFornecedor { get; set; }
    public string DescricaoProdutoFornecedor { get; set; }
    public string UnidadeMedidaFornecedor { get; set; }
    public string Moeda { get; set; }
    public decimal UltimoPrecoPraticado { get; set; }
    public decimal NovoPreco { get; set; }
}
