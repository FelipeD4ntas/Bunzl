using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.ObterAnexo;

public class OrdemDeCompraObterAnexoRequest(Guid id) : IRequest<CommandResponse<OrdemDeCompraObterAnexoResponse>>
{
	public Guid Id { get; set; } = id;
}
