using Bunzl.Core.Domain.Entities.Base;

namespace Bunzl.Domain.Entities;

public class NegociacaoComercialAnexo : EntityBase<Guid>
{
    public Guid NegociacaoComercialId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string? Observacao { get; set; }
	public virtual NegociacaoComercial? NegociacaoComercial { get; set; }

    protected NegociacaoComercialAnexo()
    {
    }

    public NegociacaoComercialAnexo(Guid negociacaoComercialId, string nome, string tipo, string observacao)
    {
        NegociacaoComercialId = negociacaoComercialId;
        Nome = nome;
        Tipo = tipo;
        Observacao = observacao;
    }
}

