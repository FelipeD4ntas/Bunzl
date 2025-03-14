using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Core.Domain.Interfaces.Base;

namespace Bunzl.Domain.Entities;

public class Incoterm : EntityBase<Guid>, IAggregationRoot
{
    public required string Sigla { get; set; }
    public required string Descricao { get; set; }
}