using System;
namespace SW.Framework.Utilities
{
    public interface IPaggingQuery
    {
        int? PageSize { get; }
        int? CurrentPageIndex { get; }
    }
}
