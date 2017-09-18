using MicroNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore.SqlServer
{
    public sealed class SqlServerRepository<TModel> : EfCoreRepository<TModel>
        where TModel : class, IModel, new()
    {
        public SqlServerRepository(DbContext context) : base(context)
        {
        }
    }
}