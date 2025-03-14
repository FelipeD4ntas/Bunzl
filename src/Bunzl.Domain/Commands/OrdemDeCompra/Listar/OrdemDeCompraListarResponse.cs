using Bunzl.Domain.DTOs;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.OrdemDeCompra.Listar;

public class OrdemDeCompraListarResponse
{
    public Guid Id { get; set; }
    public Guid FornecedorId { get; set; }
    public string? NumeroOrdem { get; set; }
    public string? NomeFornecedor { get; set; }
    public DateTime? DataOrdem { get; set; }
    public string? NumeroRevisao { get; set; }
    public DateTime? DataRevisao { get; set; }
    public decimal ValorTotal { get; set; }
	public List<OrdemDeCompraUnidadeMedidaDto>? UnidadesMedida { get; set; } 
	public EStatusOrdemDeCompra Status { get; set; }
}

