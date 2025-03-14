using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AdicionarObservacao;

public class NegociacaoComercialAdicionarObservacaoRequest(Guid id, string observacao) : IRequest<CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>>
{
	public Guid NegociacaoComercialId { get; set; } = id;
	public string Observacao { get; set; } = observacao;
}
