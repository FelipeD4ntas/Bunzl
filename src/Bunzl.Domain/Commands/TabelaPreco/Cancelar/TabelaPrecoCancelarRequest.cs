using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Cancelar;

public class TabelaPrecoCancelarRequest(Guid id) : IRequest<CommandResponse<TabelaPrecoCancelarResponse>>
{
    public Guid Id { get; set; } = id;
}