namespace Bunzl.Domain.DTOs;

public class OrdemDeCompraUnidadeMedidaDto
{
    public Guid Id { get; set; }
    public Guid OrdemDeCompraId { get; set; } 
    public string UnidadeMedida { get; set; }
    public decimal? QuantidadeTotal { get; set; }
}

