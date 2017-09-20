using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Data.Models.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddDataModels(this IServiceCollection services)
        {
            services.AddSingleton<IDataModelListProvider, DataModelListProvider>();

            return services;
        }
    }
}