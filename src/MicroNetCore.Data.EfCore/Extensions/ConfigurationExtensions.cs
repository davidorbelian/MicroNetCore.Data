using System;
using MicroNetCore.Collections;
using MicroNetCore.Data.Abstractions.Repositories;
using MicroNetCore.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Data.EfCore.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddEfCore(this IServiceCollection services,
            Type repositoryType, TypeBundle<IModel> models)
        {
            foreach (var modelType in models.Types)
                services.AddTransient(
                    GetRepositoryInterfaceType(modelType),
                    GetRepositoryImplementationType(modelType, repositoryType));

            return services;
        }

        private static Type GetRepositoryInterfaceType(Type type)
        {
            return typeof(IRepository<>).MakeGenericType(type);
        }

        private static Type GetRepositoryImplementationType(Type type, Type repository)
        {
            return repository.MakeGenericType(type);
        }
    }
}