using System.Linq;
using MicroNetCore.Data.EfCore.SqlServer.Sample.Data;
using MicroNetCore.Data.EfCore.SqlServer.Sample.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Controllers
{
    [Route("api/[controller]")]
    public sealed class SampleClassOnes : Controller
    {
        private readonly SampleContext _context;

        public SampleClassOnes(SampleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(_context.Set<SampleClassOne>().ToList());
        }
    }
}