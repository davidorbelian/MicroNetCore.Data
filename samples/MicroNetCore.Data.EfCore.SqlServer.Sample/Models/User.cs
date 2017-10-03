using System.Collections.Generic;
using MicroNetCore.Models;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Models
{
    public sealed class User : IEntityModel
    {
        public string Name { get; set; }

        public ICollection<UserRole> Roles { get; set; }
        public ICollection<UserCity> Cities { get; set; }
        public long Id { get; set; }
    }
}