using System.Text.Json.Serialization;

namespace Bunzl.Core.Domain.DTOs.Gateway;

public class GatewayProdutoOrdemDeCompraDto
{
    [JsonPropertyName("ordemItem")]
    public string? OrdemItem { get; set; }

    [JsonPropertyName("codigoItem")]
    public string? CodigoItem { get; set; }

    [JsonPropertyName("sku")]
	public string? Sku { get; set; }

	[JsonPropertyName("descricao")]
	public string? Descricao { get; set; }

	[JsonPropertyName("unidadeMedida")]
	public string? UnidadeMedida { get; set; }

	[JsonPropertyName("ncm")]
	public string? Ncm { get; set; }

	[JsonPropertyName("quantidade")]
	public decimal? Quantidade { get; set; }

    [JsonPropertyName("moeda")]
    public string? Moeda { get; set; }

    [JsonPropertyName("valorUnitario")]
	public decimal? ValorUnitario { get; set; }

	[JsonPropertyName("valorTotal")]
	public decimal? ValorTotal { get; set; }

	[JsonPropertyName("etd")]
	public DateTime? Etd { get; set; }

	[JsonPropertyName("numeroLote")]
	public string? NumeroLote { get; set; }

    [JsonPropertyName("posicaoItem")]
    public string? PosicaoItem { get; set; }

}
