using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.DeletarAnexo;

public class NegociacaoComercialDeletarAnexoRequest(Guid id, Guid anexoId) : IRequest<CommandResponse<NegociacaoComercialDeletarAnexoResponse>>
{
	public Guid Id { get; set; } = id;
    public Guid AnexoId {  get; set; } = anexoId;
}
