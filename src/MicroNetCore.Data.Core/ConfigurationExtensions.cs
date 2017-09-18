using Microsoft.Extensions.Configuration;

namespace MicroNetCore.Data.Core
{
    public static class ConfigurationExtensions
    {
        public static string GetConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("Default");
        }
    }
}