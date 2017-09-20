using MicroNetCore.Data.Models.Collections;
using MicroNetCore.Models.Collections;

namespace MicroNetCore.Data.Models
{
    public interface IDataModelListProvider
    {
        IDataModelTypeList Get(IModelTypeList types);
    }
}