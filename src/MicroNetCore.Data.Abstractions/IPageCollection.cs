using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MicroNetCore.Data.Abstractions
{
    public interface IPageCollection<TContent> : ICollection<TContent>
    {
        [DataMember]
        int PageCount { get; }

        [DataMember]
        int PageIndex { get; }

        [DataMember]
        int PageSize { get; }
    }
}