using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Entities;

public class TabelaPreco : EntityBase<Guid>, IAggregationRoot
{
    public Guid EmpresaId { get; set; }
    public Guid FornecedorId { get; set; }
    public DateTime DataInicioVigencia { get; set; }
    public DateTime DataFimVigencia { get; set; }
    public string? CodigoERP { get; set; }
    public List<TabelaPrecoProduto> Produtos { get; set; } = [];
    public EStatusTabelaPreco Status { get; set; } = EStatusTabelaPreco.AguardandoAprovacao;
    public bool FlagExpirada { get; set; }
    public virtual Fornecedor Fornecedor { get; set; }
    public virtual Empresa Empresa { get; set; }

    //EF
    protected TabelaPreco() {}

    public TabelaPreco(Guid id, Guid empresaId, Guid fornecedorId, List<TabelaPrecoProduto> produtos)
    {
        Id = id;
        EmpresaId = empresaId;
        FornecedorId = fornecedorId;
        DataInicioVigencia = DateTime.UtcNow;
        DataFimVigencia = DateTime.UtcNow.AddYears(1);
        Produtos = produtos;
        FlagExpirada = false;
    }
}
