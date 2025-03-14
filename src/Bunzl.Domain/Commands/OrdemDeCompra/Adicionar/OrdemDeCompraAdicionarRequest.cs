using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.Adicionar;

public class OrdemDeCompraAdicionarRequest(DateTime? dataFim, DateTime? dataInicio) : IRequest<CommandResponse<OrdemDeCompraAdicionarResponse>>
{
	public DateTime? DataInicio { get; set; } = dataInicio;
	public DateTime? DataFim { get; set; } = dataFim;
}


