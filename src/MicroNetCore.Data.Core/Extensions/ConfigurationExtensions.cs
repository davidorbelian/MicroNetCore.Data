using MicroNetCore.Collections;
using MicroNetCore.Data.Core.Services;
using MicroNetCore.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Data.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddDataModels(this IServiceCollection services, TypeBundle<IModel> models)
        {
            services.AddSingleton<IDataModelProvider, DataModelProvider>();

            return services;
        }
    }
}