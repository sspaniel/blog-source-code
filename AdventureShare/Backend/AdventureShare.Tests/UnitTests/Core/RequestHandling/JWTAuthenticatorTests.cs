using AdventureShare.Core.Implementations.RequestHandling;
using AdventureShare.Core.Models.Contracts;
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
        public void Authenticate_GoodToken_ReturnsClaimsPrinciple(
            UserLogin userLogin,
            IEnumerable<Permission> permissions,
            JWTAuthenticator authenticator)
        {
            // arrange
            var userToken = authenticator.CreateUserToken(userLogin, permissions);

            // act
            var claimsPrinciple = authenticator.Authenticate(userToken);

            // assert
            claimsPrinciple.Claims.ShouldContain(x => x.Type == "iss" && x.Value == "Adventure Share");
            claimsPrinciple.Claims.ShouldContain(x => x.Type == "aud" && x.Value == "Adventure Share API");
            claimsPrinciple.Claims.ShouldContain(x => x.Type == nameof(userLogin.UserId) && x.Value == userLogin.UserId.ToString());
            claimsPrinciple.Claims.ShouldContain(x => x.Type == nameof(userLogin.Email) && x.Value == userLogin.Email);
            claimsPrinciple.Claims.ShouldContain(x => x.Type == nameof(userLogin.DisplayName) && x.Value == userLogin.DisplayName);

            foreach(var permission in permissions)
            {
                claimsPrinciple.Claims.ShouldContain(x => x.Type == "Permissions" && x.Value == permission.Name);
            }
        }

        [Test, UseFakeDependencies]
        public void Authenticate_BadToken_ReturnsNull(
            UserToken badUserToken,
            IEnumerable<Permission> permissions,
            JWTAuthenticator authenticator)
        {
            // act
            var claimsPrinciple = authenticator.Authenticate(badUserToken);

            // assert
            claimsPrinciple.ShouldBeNull();
        }
    }
}
