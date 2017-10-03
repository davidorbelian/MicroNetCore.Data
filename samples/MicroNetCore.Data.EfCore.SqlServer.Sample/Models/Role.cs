using System.Collections.Generic;
using MicroNetCore.Models;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Models
{
    public sealed class Role : IEntityModel
    {
        public string Name { get; set; }

        public ICollection<UserRole> Users { get; set; }
        public long Id { get; set; }
    }
}