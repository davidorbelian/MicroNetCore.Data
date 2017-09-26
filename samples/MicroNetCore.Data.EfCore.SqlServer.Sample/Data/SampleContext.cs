using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Data
{
    public sealed class SampleContext : SqlServerContext
    {
        public SampleContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}