using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MicroNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore.Extensions
{
    public static class DbContextExtensions
    {
        public static IEnumerable<PropertyInfo> GetNavigationProperties<TModel>(this DbContext context)
            where TModel : class, IEntityModel
        {
            return context.Model
                .FindEntityType(typeof(TModel))
                .GetNavigations()
                .Select(n => n.PropertyInfo);
        }
    }
}