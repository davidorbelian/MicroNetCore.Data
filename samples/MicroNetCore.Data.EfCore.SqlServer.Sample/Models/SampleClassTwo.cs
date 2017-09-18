using MicroNetCore.Models;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Models
{
    public sealed class SampleClassTwo : IModel
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }
}