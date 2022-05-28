using AdventureShare.Core.Abstractions;
using AdventureShare.Core.Models.Contracts;
using AdventureShare.WebAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdventureShare.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRequestHandler _requestHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(
            IRequestHandler requestHandler,
            IHttpContextAccessor httpContextAccessor)
        {
            _requestHandler = requestHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] CreateUserToken request)
        { 
            var response = await _requestHandler.CreateUserTokenAsync(request);
            var actionResult = response.ToActionResult();
            return actionResult;
        }

        [HttpPut("password")]
        public async Task<IActionResult> CreateToken([FromBody] UpdateUserPassword request)
        {
            var userToken = _httpContextAccessor.HttpContext.GetUserToken();
            var response = await _requestHandler.UpdateUserPasswordAsync(request, userToken);
            var actionResult = response.ToActionResult();
            return actionResult;
        }
    }
}
