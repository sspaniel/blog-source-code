using Microsoft.AspNetCore.Mvc;
using SQLite.Web.API.Services;

namespace SQLite.Web.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository _repository;

        public UsersController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersAsync()
        {
            var users = _repository.GetUsers();
            return Ok(users);
        }
    }
}