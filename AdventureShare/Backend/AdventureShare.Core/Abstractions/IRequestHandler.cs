using AdventureShare.Core.Models.Common;
using AdventureShare.Core.Models.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace AdventureShare.Core.Abstractions
{
    public interface IRequestHandler
    {
        Task<Response<JwtSecurityToken>> LoginUserAsync(UserLoginRequest request);
    }
}
