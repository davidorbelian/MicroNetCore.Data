using System.Collections.Generic;

namespace MicroNetCore.Data.Abstractions
{
    public interface IPageCollection<TContent> : ICollection<TContent>
    {
        int PageCount { get; }
        int PageIndex { get; }
        int PageSize { get; }
    }
}