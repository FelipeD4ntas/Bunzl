using Bunzl.Core.Domain.Entities.Base;

namespace Bunzl.Domain.Entities;

public class NegociacaoComercialObservacao : EntityBase<Guid>
{
	public string Observacao { get; set; } = string.Empty;
	public Guid NegociacaoComercialId { get; set; } 
	public virtual NegociacaoComercial NegociacaoComercial { get; set; }

	protected NegociacaoComercialObservacao() { }

	public NegociacaoComercialObservacao(string observacao, Guid negociacaoComercialId)
	{
		Observacao = observacao;
		NegociacaoComercialId = negociacaoComercialId;
	}
}

