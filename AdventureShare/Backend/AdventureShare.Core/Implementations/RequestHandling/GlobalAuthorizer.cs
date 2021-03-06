using AdventureShare.Core.Abstractions.RequestHandling;
using AdventureShare.Core.Models.Contracts;
using System.Security.Claims;

namespace AdventureShare.Core.Implementations.RequestHandling
{
    public class GlobalAuthorizer : IAuthorizer
    {
        public bool IsAuthorized(UpdateUserPassword request, ClaimsPrincipal actor)
        {
            // users can only update their own password
            var isAuthorized = actor.HasClaim(nameof(request.UserId), request.UserId.ToString());
            return isAuthorized;
        }
    }
}
