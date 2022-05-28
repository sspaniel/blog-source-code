using AdventureShare.Core.Models.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace AdventureShare.WebAPI.Helpers
{
    public static class HttpContextHelper
    {
        public static UserToken GetUserToken(this HttpContext httpContext)
        {
            var hasAuthorizationHeader = httpContext.Request?.Headers?.Keys?.Contains("Authorization") ?? false;

            if (!hasAuthorizationHeader)
            {
                return null;
            }

            var authorizationHeaderValue = httpContext.Request.Headers["Authorization"]
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(authorizationHeaderValue))
            {
                return null;
            }

            var userToken = new UserToken
            {
                TokenValue = authorizationHeaderValue.Replace("bearer", string.Empty, StringComparison.CurrentCultureIgnoreCase)
            };

            return userToken;
        }
    }
}
