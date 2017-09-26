using System;
using MicroNetCore.Collections;
using MicroNetCore.Models;
using MicroNetCore.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore
{
    public abstract class EfCoreContext : DbContext
    {
        protected EfCoreContext(DbContextOptions options)
            : base(options)
        {
        }

        protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityMethod = modelBuilder.GetType().GetMethod("Entity", new Type[] { });

            foreach (var model in GetModels().Types)
                entityMethod.MakeGenericMethod(model).Invoke(modelBuilder, new object[] { });
        }

        protected TypeBundle<IModel> GetModels()
        {
            return GetType().Assembly.GetModelsTypeBundle();
        }
    }
}