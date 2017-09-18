using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore
{
    public abstract class EfCoreContext : DbContext
    {
        protected EfCoreContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}