using Bunzl.Core.Domain.DTOs.Base;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.Data.Context;

namespace Bunzl.Infra.Data.UoW;

public class UnitOfWork(BunzlContext context) : IUnitOfWork, IInjectScoped
{
    public async Task<CommitResult> CommitAsync()
    {
        try
        {
            await context.SaveChangesAsync();
            return new CommitResult(true, null, null);
        }
        catch (Exception ex)
        {
            return new CommitResult(false, ex.Message, ex.InnerException?.Message);
        }
    }
}