using Bunzl.Domain.Enumerators;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AtualizarStatus;

public class NegociacaoComercialAtualizarStatusRequest(
    Guid id,
    EStatusNegociacaoComercial status
) : IRequest<CommandResponse<NegociacaoComercialAtualizarStatusResponse>>
{
    public Guid Id { get; set; } = id;
    public EStatusNegociacaoComercial Status { get; set; } = status;
}
