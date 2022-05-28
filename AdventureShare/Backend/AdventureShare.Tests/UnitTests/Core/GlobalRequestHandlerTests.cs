using AdventureShare.Core.Abstractions.RequestHandling;
using AdventureShare.Core.Abstractions.Services;
using AdventureShare.Core.Helpers;
using AdventureShare.Core.Implementations;
using AdventureShare.Core.Models;
using AdventureShare.Core.Models.Contracts;
using AdventureShare.Core.Models.Entities;
using AdventureShare.Core.Models.Internal;
using AdventureShare.Tests.Autofixture;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureShare.Tests.UnitTests.Core
{
    internal class GlobalRequestHandlerTests
    {
        [Test, UseFakeDependencies]
        public async Task LoginUserAsync_InvalidRequest_ReturnsValidationFailed(
            [Frozen] Mock<IValidator> mockValidator,
            CreateUserToken request,
            GlobalRequestHandler requestHandler)
        {
            // arrange
            var invalidResult = new ValidationResult
            {
                Errors = new string[] { "email is required", "password is required" }
            };

            mockValidator.Setup(x => x.Validate(It.Is<CreateUserToken>(actual => actual == request)))
                .Returns(invalidResult);

            // act 
            var response = await requestHandler.CreateUserTokenAsync(request);

            // assert
            mockValidator.Verify(x => x.Validate(It.Is<CreateUserToken>(actual => actual == request)), Times.Once);

            response.Code.ShouldBe(ResponseCode.ValidationFailed);
            response.ErrorMessages.ShouldContain("email is required");
            response.ErrorMessages.ShouldContain("password is required");
            response.Data.ShouldBeNull();
        }

        [Test, UseFakeDependencies]
        public async Task LoginUserAsync_UserNotFound_ReturnsAuthenticationFailed(
            [Frozen] Mock<IValidator> mockValidator,
            [Frozen] Mock<IRepository> mockRepository,
            CreateUserToken request,
            GlobalRequestHandler requestHandler)
        {
            // arrange
            var validResult = new ValidationResult();

            mockValidator.Setup(x => x.Validate(It.Is<CreateUserToken>(actual => actual == request)))
                .Returns(validResult);

            mockRepository.Setup(x => x.GetUserLoginAsync(It.Is<string>(actual => actual == request.Email)))
                .ReturnsAsync((UserLogin)null);

            // act 
            var response = await requestHandler.CreateUserTokenAsync(request);

            // assert
            mockRepository.Verify(x => x.GetUserLoginAsync(It.Is<string>(actual => actual == request.Email)), Times.Once);

            response.Code.ShouldBe(ResponseCode.AuthenticationFailed);
            response.ErrorMessages.ShouldContain("email or password is incorrect");
            response.Data.ShouldBeNull();
        }

        [Test, UseFakeDependencies]
        public async Task LoginUserAsync_BadPassword_ReturnsAuthenticationFailed(
            [Frozen] Mock<IValidator> mockValidator,
            [Frozen] Mock<IRepository> mockRepository,
            CreateUserToken request,
            UserLogin userLogin,
            GlobalRequestHandler requestHandler)
        {
            // arrange
            var validResult = new ValidationResult();

            mockValidator.Setup(x => x.Validate(It.Is<CreateUserToken>(actual => actual == request)))
                .Returns(validResult);

            mockRepository.Setup(x => x.GetUserLoginAsync(It.Is<string>(actual => actual == request.Email)))
                .ReturnsAsync(userLogin);

            // act 
            var response = await requestHandler.CreateUserTokenAsync(request);

            // assert
            response.Code.ShouldBe(ResponseCode.AuthenticationFailed);
            response.ErrorMessages.ShouldContain("email or password is incorrect");
            response.Data.ShouldBeNull();
        }

        [Test, UseFakeDependencies]
        public async Task LoginUserAsync_ReturnsSuccess(
            [Frozen] Mock<IValidator> mockValidator,
            [Frozen] Mock<IRepository> mockRepository,
            [Frozen] Mock<IAuthenticator> mockAuthenticator,
            CreateUserToken request,
            UserLogin userLogin,
            GlobalRequestHandler requestHandler)
        {
            // arrange
            var validResult = new ValidationResult();

            mockValidator.Setup(x => x.Validate(It.Is<CreateUserToken>(actual => actual == request)))
                .Returns(validResult);

            request.Password = "just testing";
            userLogin.PasswordHash = PasswordHasher.Hash(request.Password, userLogin.CreatedUtc.ToString());

            mockRepository.Setup(x => x.GetUserLoginAsync(It.Is<string>(actual => actual == request.Email)))
                .ReturnsAsync(userLogin);

            // act 
            var response = await requestHandler.CreateUserTokenAsync(request);

            // assert
            mockRepository.Verify(x => x.GetUserPermissionsAsync(It.Is<int>(actual => actual == userLogin.UserId)), Times.Once);

            mockAuthenticator.Verify(x => x.CreateUserToken(It.Is<UserLogin>(actual => actual == userLogin), It.IsAny<IEnumerable<Permission>>()), Times.Once);

            mockRepository.Verify(x => x.UpdateUserLoginAsync(It.Is<UserLogin>(actual => actual == userLogin)), Times.Once);

            response.Code.ShouldBe(ResponseCode.Success);
            response.ErrorMessages.ShouldBeEmpty();
            response.Data.ShouldNotBeNull();
        }

        [Test, UseFakeDependencies]
        public async Task LoginUserAsync_UnexpectedError_ReturnsInternalError(
            [Frozen] Mock<IValidator> mockValidator,
            CreateUserToken request,
            GlobalRequestHandler requestHandler)
        {
            // arrange
            mockValidator.Setup(x => x.Validate(It.Is<CreateUserToken>(actual => actual == request)))
                .Throws(new Exception());

            // act 
            var response = await requestHandler.CreateUserTokenAsync(request);

            // assert
            response.Code.ShouldBe(ResponseCode.InternalError);
            response.ErrorMessages.ShouldContain("internal error, please try again and/or contact support");
            response.Data.ShouldBeNull();
        }
    }
}
