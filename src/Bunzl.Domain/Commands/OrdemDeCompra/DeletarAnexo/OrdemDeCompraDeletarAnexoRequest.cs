using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.DeletarAnexo;

public class OrdemDeCompraDeletarAnexoRequest(Guid id, Guid anexoId) : IRequest<CommandResponse<OrdemDeCompraDeletarAnexoResponse>>
{
	public Guid Id { get; set; } = id;
    public Guid AnexoId {  get; set; } = anexoId;
}
