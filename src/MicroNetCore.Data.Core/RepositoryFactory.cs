using MicroNetCore.Data.Abstractions.Repositories;
using MicroNetCore.Models;

namespace MicroNetCore.Data.Core
{
    public abstract class RepositoryFactory : IRepositoryFactory
    {
        public abstract IRepository<TModel> Create<TModel>()
            where TModel : class, IModel, new();
    }
}