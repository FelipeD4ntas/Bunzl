namespace Bunzl.Domain.DTOs;

public class NegociacaoComercialProdutoObterDto
{
	public Guid Id { get; set; }
	public Guid ProdutoId { get; set; }
    public string CodigoSku { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public decimal Quantidade { get; set; }
    public decimal ValorSugerido { get; set; }
    public string? Observacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public decimal ValorAlvo { get; set; }
    public decimal ValorFinal { get; set; }
}

