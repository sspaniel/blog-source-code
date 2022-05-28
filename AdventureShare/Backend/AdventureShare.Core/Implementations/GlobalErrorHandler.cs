using AdventureShare.Core.Abstractions;
using AdventureShare.Core.Models.Contracts;
using System;

namespace AdventureShare.Core.Implementations
{
    public class GlobalErrorHandler: IErrorHandler
    {
        public void UserLoginFailed(string message, Exception error, CreateUserToken request)
        {
            // TODO: implement
        }

        public void UpdateUserPasswordFailed(string message, Exception error, UpdateUserPassword request)
        {
            // TODO: implement
        }
    }
}
