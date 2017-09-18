using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroNetCore.AspNetCore.Paging;
using MicroNetCore.AspNetCore.ResponseExceptions.Exceptions;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Data.EfCore.Extensions;
using MicroNetCore.Models;
using MicroNetCore.Models.Markup.Attributes;
using MicroNetCore.Models.Markup.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MicroNetCore.Data.EfCore
{
    public abstract class EfCoreRepository<TModel> : IRepository<TModel>
        where TModel : class, IModel, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<TModel> _set;

        protected EfCoreRepository(DbContext context)
        {
            _context = context;
            _set = context.Set<TModel>();
        }

        #region IRepository

        public async Task<TModel> GetAsync(long id)
        {
            return await _set.FindAsync(id) ?? throw new NotFoundResponseException();
        }

        public async Task<long> PostAsync(TModel model)
        {
            await _set.AddAsync(model);
            await _context.SaveChangesAsync();

            return model.Id;
        }

        // Need to cache reflection results and generate methods for settings this values.
        public async Task PutAsync(long id, TModel model)
        {
            var dbModel = await _set.FindAsync(id) ?? throw new NotFoundResponseException();
            var properties = typeof(TModel).GetProperties().Where(p => p.HasAttribute<EditAttribute>());

            foreach (var property in properties)
            {
                var value = property.GetValue(model);

                if (value != null)
                    property.SetValue(dbModel, value);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var dbModel = await _set.FindAsync(id) ?? throw new NotFoundResponseException();

            _set.Remove(dbModel);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate)
        {
            if (predicate == null) predicate = model => true;

            return await _set
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerablePage<TModel>> FindPageAsync(int pageIndex, int pageSize,
            Expression<Func<TModel, bool>> predicate)
        {
            var set = _set
                .AsNoTracking()
                .Where(predicate);

            var resultPageCount = await set.GetPageCountAsync(pageSize);
            var resultPageIndex = pageIndex.UpdatePageIndex(resultPageCount);

            var resultModels = await set.TakePageAsync(resultPageIndex, pageSize);

            return new EnumerablePage<TModel>(resultPageCount, resultPageIndex, pageSize, resultModels);
        }

        #endregion
    }
}