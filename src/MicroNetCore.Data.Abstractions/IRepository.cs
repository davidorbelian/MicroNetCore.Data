using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MicroNetCore.Data.Abstractions
{
    public interface IRepository<TModel>
        where TModel : class, new()
    {
        Task<IEnumerable<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate = null);
        Task<TModel> GetAsync(long id);
        Task PostAsync(TModel model);
        Task PutAsync(long id, TModel model);
        Task DeleteAsync(long id);
    }
}