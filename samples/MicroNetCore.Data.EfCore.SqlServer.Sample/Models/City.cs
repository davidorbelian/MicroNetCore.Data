using System.Collections.Generic;
using MicroNetCore.Models;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Models
{
    public sealed class City : IEntityModel
    {
        public string Name { get; set; }

        public ICollection<UserCity> Users { get; set; }
        public long Id { get; set; }
    }
}