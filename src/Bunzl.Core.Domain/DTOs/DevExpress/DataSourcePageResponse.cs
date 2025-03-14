using System.Collections;

namespace Bunzl.Core.Domain.DTOs.DevExpress;

public class DataSourcePageResponse
{
    public required IEnumerable Data { get; set; }
    public int TotalCount { get; set; } = -1;
    public int GroupCount { get; set; } = -1;
    public required object[] Summary { get; set; }
}