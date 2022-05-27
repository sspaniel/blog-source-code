using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using System.IdentityModel.Tokens.Jwt;

namespace AdventureShare.Tests.Autofixture
{
    public class UseFakeDependencies : AutoDataAttribute
    {
        public UseFakeDependencies() : base(CreateFixture)
        {
        }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture();
            fixture.Register(() => new JwtSecurityToken());
            fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
            return fixture;
        }
    }
}
