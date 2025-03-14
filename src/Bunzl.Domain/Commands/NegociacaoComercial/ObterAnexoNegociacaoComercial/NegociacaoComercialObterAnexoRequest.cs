using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.ObterAnexoNegociacaoComercial;

public class NegociacaoComercialObterAnexoRequest(Guid id, Guid anexoId) : IRequest<CommandResponse<NegociacaoComercialObterAnexoResponse>>
{
	public Guid Id { get; set; } = id;
	public Guid AnexoId { get; set; } = anexoId;
}
