using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore.SqlServer
{
    public abstract class SqlServerContext : EfCoreContext
    {
        protected SqlServerContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}