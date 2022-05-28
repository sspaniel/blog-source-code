using AdventureShare.Core.Implementations.RequestHandling;
using AdventureShare.Core.Models.Contracts;
using AdventureShare.Tests.Autofixture;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Security.Claims;

namespace AdventureShare.Tests.UnitTests.Core
{
    internal class GlobalAuthorizerTests
    {
        [Test, UseFakeDependencies]
        public void UpdateUserPasswordRequest_IsAuthorized(
            UpdateUserPassword request,
            GlobalAuthorizer authorizer)
        {
            // arrange
            var claims = new List<Claim>
            {
                new Claim(nameof(request.UserId), request.UserId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims);
            var actor = new ClaimsPrincipal(claimsIdentity);

            // act
            var isAuthorized = authorizer.IsAuthorized(request, actor);

            // assert
            isAuthorized.ShouldBeTrue();
        }

        [Test, UseFakeDependencies]
        public void UpdateUserPasswordRequest_NotAuthorized(
            UpdateUserPassword request,
            GlobalAuthorizer authorizer)
        {
            // arrange
            var claims = new List<Claim>
            {
                new Claim(nameof(request.UserId), "-1")
            };

            var claimsIdentity = new ClaimsIdentity(claims);
            var actor = new ClaimsPrincipal(claimsIdentity);

            // act
            var isAuthorized = authorizer.IsAuthorized(request, actor);

            // assert
            isAuthorized.ShouldBeFalse();
        }
    }
}
