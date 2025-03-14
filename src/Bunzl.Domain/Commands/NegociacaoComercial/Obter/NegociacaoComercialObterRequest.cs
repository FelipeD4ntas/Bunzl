using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Obter;

public class NegociacaoComercialObterRequest(Guid id) : IRequest<CommandResponse<NegociacaoComercialObterResponse>>
{
	public Guid Id { get; set; } = id;
}

