using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Aprovar;

public class TabelaPrecoAprovarRequest(Guid id) : IRequest<CommandResponse<TabelaPrecoAprovarResponse>>
{
    public Guid Id { get; set; } = id;
}