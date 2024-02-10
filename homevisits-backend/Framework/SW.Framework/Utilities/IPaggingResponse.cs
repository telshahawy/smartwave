using System;
namespace SW.Framework.Utilities
{
    public interface IPaggingResponse
    {
        int TotalCount { get; }
        int? PageSize { get; }
        int? CurrentPageIndex { get; }
    }
}
