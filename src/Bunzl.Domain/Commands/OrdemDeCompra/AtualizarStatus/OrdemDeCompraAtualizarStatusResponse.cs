using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AtualizarStatus;

public class OrdemDeCompraAtualizarStatusResponse(Guid id, string mensagem, Entities.Fornecedor fornecedor, string status, string? numeroOrdemDeCompra) : BaseResponseDto(id, mensagem)
{
	public Entities.Fornecedor Fornecedor { get; set; } = fornecedor;
	public string Status { get; set; } = status;
	public string? NumeroOrdemDeCompra { get; set; } = numeroOrdemDeCompra;
}

