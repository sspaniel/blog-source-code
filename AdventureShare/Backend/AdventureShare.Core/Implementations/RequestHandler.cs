using AdventureShare.Core.Abstractions;
using AdventureShare.Core.Helpers;
using AdventureShare.Core.Models.Common;
using AdventureShare.Core.Models.Contracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace AdventureShare.Core.Implementations
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IValidator _validator;
        private readonly IRepository _repository;
        private readonly IErrorHandler _errorHandler;
        private readonly IAuthenticator _authenticator;

        public RequestHandler(
            IValidator validator,
            IRepository repository,
            IErrorHandler errorHandler,
            IAuthenticator authenticator)
        {
            _validator = validator;
            _repository = repository;
            _errorHandler = errorHandler;
            _authenticator = authenticator;
        }

        public async Task<Response<JwtSecurityToken>> LoginUserAsync(UserLoginRequest request)
        {
            try
            {
                var validationResult = _validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    var validationFailed = ResponseHelper.ValidationFailed<JwtSecurityToken>(validationResult.Errors);
                    return validationFailed;
                }

                var userLogin = await _repository.GetUserLoginAsync(request.Email);

                if (userLogin is null)
                {
                    var authenticationFailed = ResponseHelper.AuthenticationFailed<JwtSecurityToken>("email or password is incorrect");
                    return authenticationFailed;
                }

                var passwordHash = PasswordHasher.Hash(request.Password, userLogin.CreatedUtc.ToString());

                if (passwordHash != userLogin.PasswordHash)
                {
                    var authenticationFailed = ResponseHelper.AuthenticationFailed<JwtSecurityToken>("email or password is incorrect");
                    return authenticationFailed;
                }

                var userPermissions = await _repository.GetUserPermissionsAsync(userLogin.UserId);
                var securityToken = _authenticator.CreateToken(userLogin, userPermissions);

                userLogin.LastLoginUtc = DateTime.UtcNow;
                await _repository.UpdateUserLoginAsync(userLogin);

                var success = ResponseHelper.Success(securityToken);
                return success;
            }
            catch (Exception error)
            {
                await _errorHandler.UserLoginFailed(error, request);

                var internalError = ResponseHelper.InternalError<JwtSecurityToken>();
                return internalError;
            }
        }
    }
}
