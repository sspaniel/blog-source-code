using AdventureShare.Core.Implementations.RequestHandling;
using AdventureShare.Core.Models.Entities;
using AdventureShare.Tests.Autofixture;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace AdventureShare.Tests.UnitTests.Core.RequestHandling
{
    public class JWTAuthenticatorTests
    {
        [Test, UseFakeDependencies]
        public void CreateToken_Authenticate_ReturnsClaimsPrinciple(
            UserLogin userLogin,
            IEnumerable<Permission> permissions,
            JWTAuthenticator authenticator)
        {
            // arrange
            var securityToken = authenticator.CreateUserToken(userLogin, permissions);

            // act
            var claimsPrinciple = authenticator.Authenticate(securityToken);

            // assert
            claimsPrinciple.Claims.ShouldContain(x => x.Type == "iss" && x.Value == "Adventure Share");
            claimsPrinciple.Claims.ShouldContain(x => x.Type == "aud" && x.Value == "Adventure Share API");
            claimsPrinciple.Claims.ShouldContain(x => x.Type == nameof(userLogin.UserId) && x.Value == userLogin.UserId.ToString());
            claimsPrinciple.Claims.ShouldContain(x => x.Type == nameof(userLogin.Email) && x.Value == userLogin.Email);
            claimsPrinciple.Claims.ShouldContain(x => x.Type == nameof(userLogin.DisplayName) && x.Value == userLogin.DisplayName);
        }
    }
}
