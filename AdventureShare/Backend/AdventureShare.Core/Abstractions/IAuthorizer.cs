using AdventureShare.Core.Models.Contracts;
using System.Security.Claims;

namespace AdventureShare.Core.Abstractions
{
    public interface IAuthorizer
    {
        bool IsAuthorized(UpdateUserPassword request, ClaimsPrincipal actor);
    }
}
