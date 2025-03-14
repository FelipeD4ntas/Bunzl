using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Entities;

public class TabelaPrecoProduto : EntityBase<Guid>
{
    public Guid TabelaPrecoId { get; set; }
    public Guid ProdutoId { get; set; }
    public decimal UltimoPrecoPraticado { get; set; } = decimal.Zero;
    public decimal NovoPreco { get; set; } = decimal.Zero;
    public EStatusTabelaPrecoProduto Status { get; set; } = EStatusTabelaPrecoProduto.AguardandoAprovacao;
    public virtual TabelaPreco TabelaPreco { get; set; }
    public virtual FornecedorProduto Produto { get; set; }
}
