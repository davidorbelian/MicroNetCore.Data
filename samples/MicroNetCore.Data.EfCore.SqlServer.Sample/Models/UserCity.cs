using MicroNetCore.Models;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Models
{
    public sealed class UserCity : IRelationModel<User, City>
    {
        public long Entity1Id { get; set; }
        public long Entity2Id { get; set; }
        public User Entity1 { get; set; }
        public City Entity2 { get; set; }
    }
}