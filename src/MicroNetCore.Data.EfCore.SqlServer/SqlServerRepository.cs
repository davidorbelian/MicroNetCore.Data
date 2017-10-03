using MicroNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore.SqlServer
{
    public sealed class SqlServerRepository<TModel, TContext> : EfCoreRepository<TModel, TContext>
        where TModel : class, IEntityModel, new()
        where TContext : DbContext
    {
        public SqlServerRepository(TContext context) : base(context)
        {
        }
    }
}