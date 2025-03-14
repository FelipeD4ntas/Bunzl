using Bunzl.Core.Domain.Interfaces.Base;

namespace Bunzl.Core.Domain.DTOs.Base;

public class PegeResponse<TEntity>(
    int pageNumber,
    int pageSize,
    int totalRecords,
    IEnumerable<TEntity> data)
    where TEntity : IAggregationRoot
{
    public int PageNumber { get; init; } = pageNumber;
    public int PegeSize { get; init; } = pageSize;
    public int TotalRecords { get; init; } = totalRecords;
    public int TotalPages { get; init; } = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
    public IEnumerable<TEntity> Data { get; init; } = data;
}