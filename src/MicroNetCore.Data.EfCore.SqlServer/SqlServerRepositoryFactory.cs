using System;
using MicroNetCore.Data.Abstractions;

namespace MicroNetCore.Data.EfCore.SqlServer
{
    public sealed class SqlServerRepositoryFactory : EfCoreRepositoryFactory
    {
        public override IRepository<TModel> Create<TModel>()
        {
            throw new NotImplementedException();
        }
    }
}