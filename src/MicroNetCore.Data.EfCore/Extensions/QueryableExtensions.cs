using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MicroNetCore.Models;
using MicroNetCore.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<int> GetPageCountAsync<TModel>(
            this IQueryable<TModel> queryable, int pageSize)
            where TModel : class, IEntityModel
        {
            var rowCount = await queryable.CountAsync();
            return rowCount == 0 ? 1 : (rowCount + pageSize - 1) / pageSize;
        }

        public static async Task<IEnumerable<TModel>> TakePageAsync<TModel>(
            this IQueryable<TModel> queryable, int pageIndex, int pageSize)
            where TModel : class, IEntityModel
        {
            return await queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        internal static IQueryable<TModel> Include<TModel>(this IQueryable<TModel> query,
            IEnumerable<PropertyInfo> includeProperties)
            where TModel : class, IEntityModel
        {
            return includeProperties.Aggregate(query, (current, prop) => current.Include(prop));
        }

        internal static IQueryable<TModel> Include<TModel>(this IQueryable<TModel> query, PropertyInfo property)
            where TModel : class, IEntityModel
        {
            query = query.Include(property.Name);

            if (!IsRelationType(property.PropertyType)) return query;

            var relationType = GetRelationType(property.PropertyType);
            var relationProperty = relationType.GetRelationProperty(typeof(TModel));
            var relationPropertyName = relationProperty.Name;

            return query.Include($"{property.Name}.{relationPropertyName}");
        }

        private static bool IsRelationType(Type type)
        {
            return typeof(IEnumerable<IRelationModel>).IsAssignableFrom(type);
        }

        private static Type GetRelationType(Type type)
        {
            return type.GetGenericArguments().First();
        }
    }
}