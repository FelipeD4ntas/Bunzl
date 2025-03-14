using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.Obter;

public class OrdemDeCompraObterRequest(Guid id) : IRequest<CommandResponse<OrdemDeCompraObterResponse>>
{
    public Guid Id { get; set; } = id;
}

