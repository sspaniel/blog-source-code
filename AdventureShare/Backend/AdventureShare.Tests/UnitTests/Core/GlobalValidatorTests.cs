using AdventureShare.Core.Implementations.RequestHandling;
using AdventureShare.Core.Models.Contracts;
using AdventureShare.Tests.Autofixture;
using NUnit.Framework;
using Shouldly;

namespace AdventureShare.Tests.UnitTests.Core
{
    internal class GlobalValidatorTests
    {
        [Test, UseFakeDependencies]
        public void Invalid_UserLoginRequest(GlobalValidator validator)
        {
            // act
            var validationResult = validator.Validate((CreateUserToken)null);

            // assert
            validationResult.IsValid.ShouldBeFalse();
            validationResult.Errors.ShouldContain($"{nameof(CreateUserToken.Email)} is required");
            validationResult.Errors.ShouldContain($"{nameof(CreateUserToken.Password)} is required");
        }

        [Test, UseFakeDependencies]
        public void Valid_UserLoginRequest(CreateUserToken request, GlobalValidator validator)
        {
            // act 
            var validationResult = validator.Validate(request);

            // assert
            validationResult.IsValid.ShouldBeTrue();
            validationResult.Errors.ShouldBeEmpty();
        }

        [Test, UseFakeDependencies]
        public void Invalid_UpdateUserPasswordRequest(GlobalValidator validator)
        {
            // act 
            var validationResult = validator.Validate((UpdateUserPassword)null);

            // assert
            validationResult.IsValid.ShouldBeFalse();
            validationResult.Errors.ShouldContain($"{nameof(UpdateUserPassword.UserId)} is required");
            validationResult.Errors.ShouldContain($"{nameof(UpdateUserPassword.NewPassword)} is required");
        }

        [Test, UseFakeDependencies]
        public void Valid_UpdateUserPasswordRequest(UpdateUserPassword request, GlobalValidator validator)
        {
            // act 
            var validationResult = validator.Validate(request);

            // assert
            validationResult.IsValid.ShouldBeTrue();
            validationResult.Errors.ShouldBeEmpty();
        }
    }
}
