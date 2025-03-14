namespace Bunzl.Domain.Commands.NegociacaoComercial.ObterAnexoNegociacaoComercial;

public class NegociacaoComercialObterAnexoResponse(Guid id, Guid negociacaoComercialId, string nome, string tipo, DateTime dataCriacao, string? arquivoBase64 = null)
{
	public Guid Id { get; set; } = id;
	public Guid NegociacaoComercialId { get; set; } = negociacaoComercialId;
	public string Nome { get; set; } = nome;
	public string Tipo { get; set; } = tipo;
	public DateTime DataCriacao { get; set; } = dataCriacao;
	public string? ArquivoBase64 { get; set; } = arquivoBase64;
}

