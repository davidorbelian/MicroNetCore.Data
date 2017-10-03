using System;
using MicroNetCore.Collections;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Models.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Data.EfCore.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddEfCore<TContext>(this IServiceCollection services,
            Type repositoryType, TypeBundle<IModel> models)
            where TContext : DbContext
        {
            foreach (var modelType in models.Types)
                if (modelType.IsEntityModel())
                    services.AddTransient(
                        GetRepositoryInterfaceType(modelType),
                        GetRepositoryImplementationType<TContext>(modelType, repositoryType));

            return services;
        }

        private static Type GetRepositoryInterfaceType(Type type)
        {
            return typeof(IRepository<>).MakeGenericType(type);
        }

        private static Type GetRepositoryImplementationType<TContext>(Type type, Type repository)
            where TContext : DbContext
        {
            return repository.MakeGenericType(type, typeof(TContext));
        }
    }
}