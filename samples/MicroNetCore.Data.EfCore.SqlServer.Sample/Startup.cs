using System;
using MicroNetCore.AspNetCore.ConfigurationExtensions;
using MicroNetCore.Data.EfCore.SqlServer.Extensions;
using MicroNetCore.Data.EfCore.SqlServer.Sample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample
{
    public sealed class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSqlServerEfCore<SampleContext>(Configuration.GetConnectionString());
            services.AddMvc();

            return services.BuildServiceProvider();
        }
    }
}