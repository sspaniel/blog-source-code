using AdventureShare.Core.Abstractions.RequestHandling;
using AdventureShare.Core.Models.Contracts;
using AdventureShare.Core.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdventureShare.Core.Implementations
{
    public class JWTAuthenticator : IAuthenticator
    {
        private const string ISSUER = "Adventure Share";
        private const string AUDIENCE = "Adventure Share API";

        private readonly byte[] _signatureKey;
        private readonly int _expirationDurationInHours;

        public JWTAuthenticator(
            string signatureKey,
            int expirationDurationInHours)
        {
            _signatureKey = Encoding.UTF8.GetBytes(signatureKey);
            _expirationDurationInHours = expirationDurationInHours;
        }

        public UserToken CreateUserToken(UserLogin userLogin, IEnumerable<Permission> permissions)
        {
            var expires = DateTime.UtcNow.AddHours(_expirationDurationInHours);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, ISSUER),
                new Claim(JwtRegisteredClaimNames.Aud, AUDIENCE),
                new Claim(nameof(userLogin.UserId), userLogin.UserId.ToString()),
                new Claim(nameof(userLogin.Email), userLogin.Email),
                new Claim(nameof(userLogin.DisplayName), userLogin.DisplayName),
            };

            foreach (var permission in permissions)
            {
                var claim = new Claim("Permissions", permission.Name);
                claims.Add(claim);
            }

            var key = new SymmetricSecurityKey(_signatureKey);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                issuer: ISSUER,
                expires: expires,
                claims: claims,
                signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            var userToken = new UserToken
            {
                UserId = userLogin.UserId,
                Email = userLogin.Email,
                DisplayName = userLogin.DisplayName,
                ExpiresUtc = expires.ToString("u"),
                TokenValue = tokenHandler.WriteToken(securityToken)
            };

            return userToken;
        }

        public ClaimsPrincipal Authenticate(UserToken userToken)
        {
            if (string.IsNullOrWhiteSpace(userToken?.TokenValue))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(userToken.TokenValue))
            {
                return null;
            }

            var validationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = AUDIENCE,
                ValidateIssuer = true,
                ValidIssuer = ISSUER,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(_signatureKey)
            };

            var claimsPrincipal = tokenHandler.ValidateToken(userToken.TokenValue, validationParameters, out SecurityToken validatedToken);
            return claimsPrincipal;
        }
    }
}
