namespace MicroNetCore.Data.Abstractions
{
    public interface IEntityDataModel : IDataModel
    {
        long Id { get; }
    }
}