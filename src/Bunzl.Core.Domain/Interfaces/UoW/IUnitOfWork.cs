using Bunzl.Core.Domain.DTOs.Base;

namespace Bunzl.Core.Domain.Interfaces.UoW;

public interface IUnitOfWork
{
    Task<CommitResult> CommitAsync();
}