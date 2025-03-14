namespace Bunzl.Domain.DTOs;

public class MoedaDto
{
    public Guid Id { get; set; }
    public required string Sigla { get; set; }
    public required string Descricao { get; set; }
}