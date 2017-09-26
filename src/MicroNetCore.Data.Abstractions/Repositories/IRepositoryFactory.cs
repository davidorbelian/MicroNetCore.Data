using MicroNetCore.Models;

namespace MicroNetCore.Data.Abstractions.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<TModel> Create<TModel>()
            where TModel : class, IModel, new();
    }
}