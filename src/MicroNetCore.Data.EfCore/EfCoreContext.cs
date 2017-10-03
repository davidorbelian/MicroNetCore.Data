using System;
using System.Reflection;
using MicroNetCore.Collections;
using MicroNetCore.Models;
using MicroNetCore.Models.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNetCore.Data.EfCore
{
    public abstract class EfCoreContext : DbContext
    {
        private static readonly MethodInfo EntityMethod =
            typeof(ModelBuilder).GetMethod(nameof(ModelBuilder.Entity), new Type[] { });

        private static readonly MethodInfo HasKeyMethod =
            typeof(EntityTypeBuilder).GetMethod(nameof(EntityTypeBuilder.HasKey), new[] {typeof(string[])});

        protected EfCoreContext(DbContextOptions options)
            : base(options)
        {
        }

        protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var modelType in GetModels().Types)
                AddModel(modelBuilder, modelType);
        }

        private TypeBundle<IModel> GetModels()
        {
            return GetType().Assembly.GetModelsTypeBundle();
        }

        private static void AddModel(ModelBuilder modelBuilder, Type modelType)
        {
            if (modelType.IsEntityModel())
                AddEntityModel(modelBuilder, modelType);
            else if (modelType.IsRelationModel())
                AddRelationModel(modelBuilder, modelType);
            else
                throw new Exception($"{modelType.Name} has unknown model type.");
        }

        private static void AddEntityModel(ModelBuilder modelBuilder, Type modelType)
        {
            EntityMethod.MakeGenericMethod(modelType).Invoke(modelBuilder, new object[] { });
        }

        private static void AddRelationModel(ModelBuilder modelBuilder, Type modelType)
        {
            var entityTypeBuilder = EntityMethod
                .MakeGenericMethod(modelType)
                .Invoke(modelBuilder, new object[] { });

            var args = new[] {nameof(IRelationModel.Entity1Id), nameof(IRelationModel.Entity2Id)};
            HasKeyMethod.Invoke(entityTypeBuilder, new object[] {args});
        }
    }
}