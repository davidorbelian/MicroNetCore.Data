using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroNetCore.Models;

namespace MicroNetCore.Data.Abstractions
{
    public interface IRepository<TModel>
        where TModel : class, IModel, new()
    {
        Task<IEnumerable<TModel>> FindAsync(
            Expression<Func<TModel, bool>> predicate = null);

        Task<IPageCollection<TModel>> FindPageAsync(
            int pageIndex, int pageSize,
            Expression<Func<TModel, bool>> predicate = null);

        Task<TModel> GetAsync(long id);
        Task<long> PostAsync(TModel model);
        Task PutAsync(long id, TModel model);
        Task DeleteAsync(long id);
    }
}