using AdventureShare.Core.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AdventureShare.WebAPI.Helpers
{
    public static class ResponseHelper
    {
        public static IActionResult ToActionResult<TResponse>(this Response<TResponse> response)
        {
            switch (response.Code)
            {
                case ResponseCode.Success:
                    var ok = new ObjectResult(response);
                    ok.StatusCode = (int)HttpStatusCode.OK;
                    return ok;
                case ResponseCode.AuthenticationFailed:
                    var forbidden = new ObjectResult(response);
                    forbidden.StatusCode = (int)HttpStatusCode.Forbidden;
                    return forbidden;
                case ResponseCode.ValidationFailed:
                    var badRequest = new ObjectResult(response);
                    badRequest.StatusCode = (int)HttpStatusCode.BadRequest;
                    return badRequest;
                case ResponseCode.AuthorizationFailed:
                    var unauthorized = new ObjectResult(response);
                    unauthorized.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return unauthorized;
                default:
                    var internalServerError = new ObjectResult(response);
                    internalServerError.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return internalServerError;
            }
        }
    }
}
