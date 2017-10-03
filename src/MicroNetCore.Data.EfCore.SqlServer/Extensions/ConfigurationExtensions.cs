using MicroNetCore.Data.EfCore.Extensions;
using MicroNetCore.Models.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Data.EfCore.SqlServer.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddSqlServerEfCore<TContext>(this IServiceCollection services,
            string connectionString)
            where TContext : DbContext
        {
            var modelsTypeBundle = typeof(TContext).Assembly.GetModelsTypeBundle();

            services.AddSingleton(modelsTypeBundle);

            services.AddDbContext<TContext>(o => o.UseSqlServer(connectionString));
            services.AddEfCore<TContext>(typeof(SqlServerRepository<,>), modelsTypeBundle);

            return services;
        }
    }
}