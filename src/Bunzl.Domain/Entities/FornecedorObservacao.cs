using Bunzl.Core.Domain.Entities.Base;

namespace Bunzl.Domain.Entities;

public class FornecedorObservacao : EntityBase<Guid>
{
    public string Observacao { get; set; } = string.Empty;
    public Guid FornecedorId  { get; set; }
    public virtual Fornecedor Fornecedor { get; set; }
}