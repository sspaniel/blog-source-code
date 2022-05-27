using AutoFixture;
using AutoFixture.NUnit3;

namespace AdventureShare.Tests.Autofixture
{
    public class UseRealDependencies : AutoDataAttribute
    {
        public UseRealDependencies() : base(CreateFixture)
        {
        }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture();
            return fixture;
        }
    }
}
