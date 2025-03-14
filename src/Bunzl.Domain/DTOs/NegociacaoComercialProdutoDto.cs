namespace Bunzl.Domain.DTOs;

public class NegociacaoComercialProdutoDto
{
    public Guid ProdutoId { get; set; }
    public string CodigoSku { get; set; }
    public string Descricao { get; set; }
    public decimal Quantidade { get; set; }
    public decimal ValorUnitarioOriginal { get; set; }
    public decimal ValorUnitarioNegociado { get; set; }
    public decimal ValorUnitarioAlvo { get; set; }
    public decimal ValorUnitarioFinal { get; set; }
    public string? Observacao { get; set; }
}