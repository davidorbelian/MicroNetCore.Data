using System.Threading.Tasks;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Data.EfCore.SqlServer.Sample.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroNetCore.Data.EfCore.SqlServer.Sample.Controllers
{
    [Route("api/users")]
    public sealed class UsersController : Controller
    {
        private readonly IRepository<User> _users;

        public UsersController(IRepository<User> users)
        {
            _users = users;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _users.FindAsync());
        }
    }
}