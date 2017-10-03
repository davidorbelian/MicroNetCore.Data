using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MicroNetCore.Models;
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
            IEnumerable<PropertyInfo> inclideProperties)
            where TModel : class, IEntityModel
        {
            return inclideProperties.Aggregate(query, (current, prop) => current.Include(prop));
        }

        internal static IQueryable<TModel> Include<TModel>(this IQueryable<TModel> query, PropertyInfo property)
            where TModel : class, IEntityModel
        {
            query.Include(property.Name);

            var relationType = GetRelationType(property.PropertyType);

            return relationType != null
                ? query.Include($"{property.Name}.{GetIncludePropName(relationType, typeof(TModel))}")
                : query;
        }

        private static Type GetRelationType(Type type)
        {
            return typeof(IEnumerable<IRelationModel>).IsAssignableFrom(type)
                ? type.GetGenericArguments().First()
                : null;
        }

        private static string GetIncludePropName(Type relationType, Type originalType)
        {
            var generics = relationType
                .GetInterfaces()
                .Single(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRelationModel<,>))
                .GetGenericArguments();

            if (generics[0] == originalType)
                return "Entity2";

            if (generics[1] == originalType)
                return "Entity1";

            throw new IndexOutOfRangeException(
                $"Can not found ThenIncludePropName for {originalType.Name} and {relationType.Name}");
        }
    }
}