﻿using MicroNetCore.Data.Core;
using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore.SqlServer
{
    public abstract class SqlServerContextFactory<TContext> : EfCoreContextFactory<TContext>
        where TContext : SqlServerContext
    {
        public override TContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString());

            return GetContext(optionsBuilder.Options);
        }

        protected abstract TContext GetContext(DbContextOptions<TContext> options);
    }
}