using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;

namespace MicroNetCore.Data.Core
{
    public abstract class RepositoryFactory : IRepositoryFactory
    {
        public abstract IRepository<TModel> Create<TModel>()
            where TModel : class, IEntityModel, new();
    }
}