namespace Bunzl.Domain.DTOs;

public class OrdemDeCompraAnexoDto
{
    public Guid Id { get; set; }
    public Guid OrdemDeCompraId { get; set; }
    public string Nome { get; set; } 
    public string Tipo { get; set; }
    public string? Observacao { get; set; }
    public DateTime DataCriacao { get; set; }
}
