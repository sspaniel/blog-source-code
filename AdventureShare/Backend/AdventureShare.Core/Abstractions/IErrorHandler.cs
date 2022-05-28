using AdventureShare.Core.Models.Contracts;
using System;

namespace AdventureShare.Core.Abstractions
{
    public interface IErrorHandler
    {
        void UserLoginFailed(string message, Exception error, CreateUserToken request);
        void UpdateUserPasswordFailed(string message, Exception error, UpdateUserPassword request);
    }
}
