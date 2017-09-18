using MicroNetCore.Data.EfCore.SqlServer.Sample.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Data
{
    public sealed class SampleContext : SqlServerContext
    {
        public SampleContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SampleClassOne>();
            modelBuilder.Entity<SampleClassTwo>();
        }
    }
}