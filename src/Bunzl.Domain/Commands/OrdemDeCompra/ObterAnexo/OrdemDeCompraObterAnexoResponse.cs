namespace Bunzl.Domain.Commands.OrdemDeCompra.ObterAnexo;

public class OrdemDeCompraObterAnexoResponse(Guid id, Guid ordemDeCompraId, string nome, string tipo, DateTime dataCriacao, string? arquivoBase64 = null)
{
	public Guid Id { get; set; } = id;
	public Guid OrdemDeCompraId { get; set; } = ordemDeCompraId;
	public string Nome { get; set; } = nome;
	public string Tipo { get; set; } = tipo;
	public DateTime DataCriacao { get; set; } = dataCriacao;
	public string? Base64 { get; set; } = arquivoBase64;
}

