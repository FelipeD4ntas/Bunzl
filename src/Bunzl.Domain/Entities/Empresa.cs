using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Core.Domain.Interfaces.Base;

namespace Bunzl.Domain.Entities;

public class Empresa : EntityBase<Guid>, IAggregationRoot
{
    public string Nome { get; set; } = string.Empty;
    public string? EmpresaBunzlId { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public bool FlagRegravarTabelaPrecoErp { get; set; } = false;
    public string? SiglaEmpresa { get; set; }
    public DateTime? DataUltimaAtualizacao { get; set; }
    public DateTime? DataUltimaAtualizacaoOrdemDeCompra { get; set; }
	public List<Usuario> Usuarios { get; set; } = [];
    public List<Fornecedor> Fornecedores { get; set; } = [];
}