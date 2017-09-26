using MicroNetCore.AspNetCore.ConfigurationExtensions;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MicroNetCore.Data.EfCore
{
    public abstract class EfCoreContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : EfCoreContext
    {
        protected readonly string ConnectionString;

        protected EfCoreContextFactory()
        {
            ConnectionString = new ConfigurationBuilder()
                .AddSettingsFolder()
                .Build()
                .GetConnectionString();
        }

        public abstract TContext CreateDbContext(string[] args);
    }
}