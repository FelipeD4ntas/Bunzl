namespace Bunzl.Domain.DTOs;

public class OrdemDeCompraProdutoDto
{
    public Guid Id { get; set; }
    public Guid OrdemDeCompraId { get; set; }
    public string? OrdemItem { get; set; }
    public string? CodigoItem { get; set; }
    public string? CodigoSKU { get; set; }
    public string? Descricao { get; set; }
    public string? UnidadeMedida { get; set; }
    public string? CodigoNCM { get; set; }
    public decimal? Quantidade { get; set; }
    public string? MoedaSigla { get; set; }
    public decimal? ValorUnitario { get; set; }
    public decimal? ValorTotal { get; set; }
    public DateTime? DataEstimadaPartida { get; set; }
    public DateTime? Etd { get; set; }
    public string? NumeroLote { get; set; }
    public string? PosicaoItem { get; set; }
}
