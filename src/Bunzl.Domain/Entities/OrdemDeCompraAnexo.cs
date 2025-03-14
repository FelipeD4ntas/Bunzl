using Bunzl.Core.Domain.Entities.Base;

namespace Bunzl.Domain.Entities;

public class OrdemDeCompraAnexo(string nome, string tipo, Guid ordemDeCompraId)
    : EntityBase<Guid>
{
    public string Nome { get; set; } = nome;
    public string Tipo { get; set; } = tipo;
    public Guid OrdemDeCompraId { get; set; } = ordemDeCompraId;
    public virtual OrdemDeCompra OrdemDeCompra { get; set; }
}

