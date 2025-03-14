namespace Bunzl.Domain.Commands.OrdemDeCompra.AdicionarObservacao;

public class OrdemDeCompraAdicionarObservacaoPayload
{
	public required string Observacao { get; set; }

	public OrdemDeCompraAdicionarObservacaoRequest ToRequest(Guid ordemDeCompraId)
    {
		return new OrdemDeCompraAdicionarObservacaoRequest(ordemDeCompraId, Observacao);
	}
}

