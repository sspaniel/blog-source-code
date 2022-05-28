using AdventureShare.Core.Abstractions.RequestHandling;
using AdventureShare.Core.Abstractions.Services;
using AdventureShare.Core.Helpers;
using AdventureShare.Core.Models.Contracts;
using System;
using System.Threading.Tasks;

namespace AdventureShare.Core.Implementations.RequestHandling
{
    public class GlobalRequestHandler : IRequestHandler
    {
        private readonly IValidator _validator;
        private readonly IRepository _repository;
        private readonly IErrorHandler _errorHandler;
        private readonly IAuthenticator _authenticator;
        private readonly IAuthorizer _authorizer;

        public GlobalRequestHandler(
            IValidator validator,
            IRepository repository,
            IErrorHandler errorHandler,
            IAuthenticator authenticator,
            IAuthorizer authorizer)
        {
            _validator = validator;
            _repository = repository;
            _errorHandler = errorHandler;
            _authenticator = authenticator;
            _authorizer = authorizer;
        }

        public async Task<Response<UserToken>> CreateUserTokenAsync(CreateUserToken request)
        {
            try
            {
                var validationResult = _validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    var validationFailed = ResponseFormatter.ValidationFailed<UserToken>(validationResult.Errors);
                    return validationFailed;
                }

                var userLogin = await _repository.GetUserLoginAsync(request.Email);

                if (userLogin is null)
                {
                    var authenticationFailed = ResponseFormatter.AuthenticationFailed<UserToken>("email or password is incorrect");
                    return authenticationFailed;
                }

                var passwordHash = PasswordHasher.Hash(request.Password, userLogin.CreatedUtc.ToString());

                if (passwordHash != userLogin.PasswordHash)
                {
                    var authenticationFailed = ResponseFormatter.AuthenticationFailed<UserToken>("email or password is incorrect");
                    return authenticationFailed;
                }

                var userPermissions = await _repository.GetUserPermissionsAsync(userLogin.UserId);
                var userToken = _authenticator.CreateUserToken(userLogin, userPermissions);

                userLogin.LastLoginUtc = DateTime.UtcNow;
                await _repository.UpdateUserLoginAsync(userLogin);

                var success = ResponseFormatter.Success(userToken);
                return success;
            }
            catch (Exception error)
            {
                _errorHandler.CreateUserTokenFailed("create user token failed", error, request);
                var internalError = ResponseFormatter.InternalError<UserToken>();
                return internalError;
            }
        }

        public async Task<Response<string>> UpdateUserPasswordAsync(UpdateUserPassword request, UserToken userToken)
        {
            try
            {
                var actor = _authenticator.Authenticate(userToken);

                if (actor is null)
                {
                    var authenticationFailed = ResponseFormatter.AuthenticationFailed<string>("authentication failed");
                    return authenticationFailed;
                }

                var validationResult = _validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    var validationFailed = ResponseFormatter.ValidationFailed<string>(validationResult.Errors);
                    return validationFailed;
                }

                var isAuthorized = _authorizer.IsAuthorized(request, actor);

                if (!isAuthorized)
                {
                    var authorizationFailed = ResponseFormatter.AuthorizationFailed<string>();
                    return authorizationFailed;
                }

                var userLogin = await _repository.GetUserLoginAsync(request.UserId);
                userLogin.PasswordHash = PasswordHasher.Hash(request.NewPassword, userLogin.CreatedUtc.ToString());

                await _repository.UpdateUserLoginAsync(userLogin);

                var success = ResponseFormatter.Success("password updated");
                return success;
            }
            catch (Exception error)
            {
                _errorHandler.UpdateUserPasswordFailed("user password update failed", error, request);
                var internalError = ResponseFormatter.InternalError<string>();
                return internalError;
            }
        }
    }
}
