using AdventureShare.Core.Models.Common;
using System.Collections.Generic;

namespace AdventureShare.Core.Helpers
{
    public static class ResponseHelper
    {
        public static Response<TData> Success<TData>(TData data)
        {
            var response = new Response<TData>
            {
                Code = ResponseCode.Success,
                Data = data
            };

            return response;
        }

        public static Response<TData> AuthenticationFailed<TData>(string message)
        {
            var response = new Response<TData>
            {
                Code = ResponseCode.AuthenticationFailed,
                Errors = new string[] { message }
            };

            return response;
        }

        public static Response<TData> ValidationFailed<TData>(IEnumerable<string> validationErrors)
        {
            var response = new Response<TData>
            {
                Code = ResponseCode.ValidationFailed,
                Errors = validationErrors
            };

            return response;
        }

        public static Response<TData> AuthorizationFailed<TData>()
        {
            var response = new Response<TData>
            {
                Code = ResponseCode.AuthenticationFailed,
                Errors = new string[] { "user does not have permission" },
            };

            return response;
        }

        public static Response<TData> InternalError<TData>()
        {
            var response = new Response<TData>
            {
                Code = ResponseCode.InternalError,
                Errors = new string[] { "internal error, please try again and/or contact support" }
            };

            return response;
        }
    }
}
