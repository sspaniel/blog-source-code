using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdventureShare.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] object credentials)
        {
            return await Task.FromResult(Ok());
        }
    }
}
