using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Entities;

public class Auditoria : EntityBase<Guid>, IAggregationRoot
{
    public required Guid EntidadeId { get; set; }
    public required string EntidadeNome { get; set; }
    public required string Funcao { get; set; }
    public ETipoAuditoria TipoAuditoria { get; set; }
}
