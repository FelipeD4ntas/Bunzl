using Bunzl.Core.Domain.Entities.Base;

namespace Bunzl.Domain.Entities;

public class OrdemDeCompraObservacao(string observacao, Guid ordemDeCompraId) : EntityBase<Guid>
{
    public string Observacao { get; set; } = observacao;
    public Guid OrdemDeCompraId { get; set; } = ordemDeCompraId;
    public virtual OrdemDeCompra? OrdemDeCompra { get; set; }
}

