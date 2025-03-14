namespace Bunzl.Domain.DTOs;

public class NegociacaoComercialAnexoListarDto(Guid id, string nome, string tipo, string observacao, Guid negociacaoComercialId, DateTime dataCriacao, string base64 = "")
{
    public Guid Id { get; set; } = id;
    public string Nome { get; set; } = nome;
    public string Tipo { get; set; } = tipo;
    public string? Observacao { get; set; } = observacao;
    public Guid NegociacaoComercialId { get; set; } = negociacaoComercialId;
    public DateTime DataCriacao { get; set; } = dataCriacao;
    public string? Base64 { get; set; } = base64;
}

