using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<int> GetPageCountAsync<TDataModel>(this IQueryable<TDataModel> queryable, int pageSize)
        {
            var rowCount = await queryable.CountAsync();
            return rowCount == 0 ? 1 : (rowCount + pageSize - 1) / pageSize;
        }

        public static async Task<IEnumerable<TDataModel>> TakePageAsync<TDataModel>(
            this IQueryable<TDataModel> queryable,
            int pageIndex, int pageSize)
        {
            return await queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}