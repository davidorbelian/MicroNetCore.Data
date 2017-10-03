using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Models;

namespace MicroNetCore.Data.Abstractions
{
    public interface IRepository<TModel>
        where TModel : class, IEntityModel, new()
    {
        Task<ICollection<TModel>> FindAsync(
            Expression<Func<TModel, bool>> predicate = null);

        Task<IEnumerablePage<TModel>> FindPageAsync(
            int pageIndex, int pageSize,
            Expression<Func<TModel, bool>> predicate = null);

        Task<TModel> GetAsync(long id);
        Task<long> PostAsync(TModel model);
        Task PutAsync(long id, TModel model);
        Task DeleteAsync(long id);
    }
}