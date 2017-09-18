using MicroNetCore.Data.Abstractions;
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
            services.AddTransient(typeof(IRepository<>), typeof(SqlServerRepository<>));
            services.AddDbContext<TContext>(o => o.UseSqlServer(connectionString));

            return services;
        }
    }
}