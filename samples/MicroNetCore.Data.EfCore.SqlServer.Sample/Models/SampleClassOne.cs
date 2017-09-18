using MicroNetCore.Models;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Models
{
    public sealed class SampleClassOne : IModel
    {
        public string Name { get; set; }

        public long SampleClassTwoId { get; set; }
        public SampleClassTwo SampleClassTwo { get; set; }
        public long Id { get; set; }
    }
}