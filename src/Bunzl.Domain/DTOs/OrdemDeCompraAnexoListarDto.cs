namespace Bunzl.Domain.DTOs;

public class OrdemDeCompraAnexoListarDto(Guid id, string nome, string tipo, Guid ordemDeCompraId, DateTime dataCriacao, string base64 = "")
{
    public Guid Id { get; set; } = id;
    public string Nome { get; set; } = nome;
    public string Tipo { get; set; } = tipo;
    public Guid OrdemDeCompraId { get; set; } = ordemDeCompraId;
    public DateTime DataCriacao { get; set; } = dataCriacao;
    public string? Base64 { get; set; } = base64;
}

