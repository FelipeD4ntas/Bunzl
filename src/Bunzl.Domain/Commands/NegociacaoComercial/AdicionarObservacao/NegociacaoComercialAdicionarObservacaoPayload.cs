namespace Bunzl.Domain.Commands.NegociacaoComercial.AdicionarObservacao;

public class NegociacaoComercialAdicionarObservacaoPayload
{
	public required string Observacao { get; set; }

	public NegociacaoComercialAdicionarObservacaoRequest ToRequest(Guid negociacaoComercialId)
	{
		return new NegociacaoComercialAdicionarObservacaoRequest(negociacaoComercialId, Observacao);
	}
}

