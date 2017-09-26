using System;
using System.Collections.Generic;
using MicroNetCore.Collections;
using MicroNetCore.Models;

namespace MicroNetCore.Data.Core.Services
{
    public interface IDataModelProvider
    {
        IEnumerable<Type> Get(TypeBundle<IModel> types);
    }
}