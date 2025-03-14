using Bunzl.Domain.Enumerators;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AtualizarStatus;

public class OrdemDeCompraAtualizarStatusRequest(Guid id, EStatusOrdemDeCompra status) : IRequest<CommandResponse<OrdemDeCompraAtualizarStatusResponse>>
{
	public Guid Id { get; set; } = id;
	public EStatusOrdemDeCompra Status { get; set; } = status;
}

