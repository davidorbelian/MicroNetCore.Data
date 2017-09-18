using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Data
{
    public sealed class SampleContextFactory : SqlServerContextFactory<SampleContext>
    {
        protected override SampleContext GetContext(DbContextOptions<SampleContext> options)
        {
            return new SampleContext(options);
        }
    }
}