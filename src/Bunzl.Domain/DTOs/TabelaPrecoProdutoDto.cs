using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.DTOs;

public class TabelaPrecoProdutoDto
{
    public Guid Id { get; set; }
    public Guid TabelaPrecoId { get; set; }
    public string? CodigoSku { get; set; } 
    public string? DescricaoProdutoBunzl { get; set; }
    public string? CodigoProdutoFornecedor { get; set; } 
    public string? UnidadeMedidaFornecedorPreco { get; set; }
    public decimal UltimoPrecoPraticado { get; set; }
    public decimal NovoPreco { get; set; }
    public EStatusTabelaPrecoProduto Status { get; set; }
}