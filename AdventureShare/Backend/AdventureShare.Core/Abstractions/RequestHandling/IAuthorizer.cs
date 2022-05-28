using AdventureShare.Core.Models.Contracts;
using System.Security.Claims;

namespace AdventureShare.Core.Abstractions.RequestHandling
{
    public interface IAuthorizer
    {
        bool IsAuthorized(UpdateUserPassword request, ClaimsPrincipal actor);
    }
}
