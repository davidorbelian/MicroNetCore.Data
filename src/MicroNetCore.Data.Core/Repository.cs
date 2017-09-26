using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.Data.Abstractions.Repositories;
using MicroNetCore.Models;

namespace MicroNetCore.Data.Core
{
    public abstract class Repository<TModel> : IRepository<TModel>
        where TModel : class, IModel, new()
    {
        #region IRepository

        public abstract Task<ICollection<TModel>> FindAsync(
            Expression<Func<TModel, bool>> predicate);

        public abstract Task<IEnumerablePage<TModel>> FindPageAsync(int pageIndex, int pageSize,
            Expression<Func<TModel, bool>> predicate);

        public abstract Task<TModel> GetAsync(long id);

        public abstract Task<long> PostAsync(TModel model);

        public abstract Task PutAsync(long id, TModel model);

        public abstract Task DeleteAsync(long id);

        #endregion
    }
}