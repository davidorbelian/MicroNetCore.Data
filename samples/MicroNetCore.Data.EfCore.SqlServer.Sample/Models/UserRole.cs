using MicroNetCore.Models;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Models
{
    public sealed class UserRole : IRelationModel<User, Role>
    {
        public User Entity1 { get; set; }
        public Role Entity2 { get; set; }
        public long Entity1Id { get; set; }
        public long Entity2Id { get; set; }
    }
}