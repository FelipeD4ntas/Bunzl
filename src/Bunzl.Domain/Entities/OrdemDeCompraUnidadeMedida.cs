using Bunzl.Core.Domain.Entities.Base;

namespace Bunzl.Domain.Entities;

public class OrdemDeCompraUnidadeMedida : EntityBase<Guid>
{
    public Guid OrdemDeCompraId { get; set; }
    public string UnidadeMedida { get; set; } = string.Empty;
    public decimal? QuantidadeTotal { get; set; }
    public virtual OrdemDeCompra OrdemDeCompra { get; set; }

    protected OrdemDeCompraUnidadeMedida() { }

    public OrdemDeCompraUnidadeMedida(OrdemDeCompra ordemDeCompra, string unidadeMedida, decimal? quantidadeTotal)
    {
        OrdemDeCompra = ordemDeCompra;
        UnidadeMedida = unidadeMedida;
        QuantidadeTotal = quantidadeTotal;
    }
}

