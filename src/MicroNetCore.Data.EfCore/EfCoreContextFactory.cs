using MicroNetCore.AspNetCore.ConfigurationExtensions;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MicroNetCore.Data.EfCore
{
    public abstract class EfCoreContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : EfCoreContext
    {
        protected EfCoreContextFactory()
        {
            var builder = new ConfigurationBuilder()
                .AddSettingsFolder();

            Configuration = builder.Build();
        }

        protected IConfiguration Configuration { get; }

        public abstract TContext CreateDbContext(string[] args);
    }
}