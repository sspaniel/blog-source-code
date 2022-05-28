using AdventureShare.Core.Models.Contracts;
using AdventureShare.Core.Models.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace AdventureShare.Core.Abstractions
{
    public interface IAuthenticator
    {
        UserToken CreateUserToken(UserLogin userLogin, IEnumerable<Permission> permissions);

        ClaimsPrincipal Authenticate(UserToken userToken);
    }
}
