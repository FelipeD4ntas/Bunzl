using System.Text.Json.Serialization;

namespace Bunzl.Core.Domain.DTOs.Gateway;

public class GatewayRetornoTabelaPrecoDto
{
	[JsonPropertyName("codigoTabelaPreco")]
	public string? CodigoTabelaPreco { get; set; }

	[JsonPropertyName("message")]
	public string? Message { get; set; }
}
