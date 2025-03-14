using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Entities;

public class FornecedorProdutoAnexo : EntityBase<Guid>
{
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public ETipoDocumento TipoDocumento { get; set; }
    public string? Observacao { get; set; }
    public Guid FornecedorProdutoId { get; set; }
    public virtual FornecedorProduto? FornecedorProduto { get; set; }
}
