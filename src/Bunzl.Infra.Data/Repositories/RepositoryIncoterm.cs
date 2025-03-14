using Bunzl.Domain.Entities;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.Data.Context;
using Bunzl.Infra.Data.Repositories.Base;

namespace Bunzl.Infra.Data.Repositories
{
    public class RepositoryIncoterm(BunzlContext context) : RepositoryBase<Incoterm, BunzlContext, Guid>(context), IRepositoryIncoterm, IInjectScoped;
}