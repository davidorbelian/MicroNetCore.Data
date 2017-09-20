using System;
using System.Collections.Generic;
using System.Linq;
using MicroNetCore.Data.Abstractions;

namespace MicroNetCore.Data.Models.Collections
{
    public sealed class DataModelTypeList : List<Type>, IDataModelTypeList
    {
        public DataModelTypeList(IEnumerable<Type> types)
            : base(ValidateTypes(types))
        {
        }

        private static IEnumerable<Type> ValidateTypes(IEnumerable<Type> types)
        {
            return types.Where(IsDataModelType);
        }

        private static bool IsDataModelType(Type type)
        {
            return typeof(IDataModel).IsAssignableFrom(type);
        }
    }
}