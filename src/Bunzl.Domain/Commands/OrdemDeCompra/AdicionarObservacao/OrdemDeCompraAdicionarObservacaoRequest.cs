using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AdicionarObservacao;

public class OrdemDeCompraAdicionarObservacaoRequest(Guid id, string observacao) : IRequest<CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>>
{
	public Guid OrdemDeCompraId { get; set; } = id;
	public string Observacao { get; set; } = observacao;
}
