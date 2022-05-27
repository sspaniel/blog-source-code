using AdventureShare.Core.Models.Entities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace AdventureShare.Core.Abstractions
{
    public interface IAuthenticator
    {
        JwtSecurityToken CreateToken(UserLogin userLogin, IEnumerable<Permission> permissions);

        bool IsAuthenticated(JwtSecurityToken securityToken);
    }
}
