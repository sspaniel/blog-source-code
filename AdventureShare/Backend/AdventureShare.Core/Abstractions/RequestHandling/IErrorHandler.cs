using AdventureShare.Core.Models.Contracts;
using System;

namespace AdventureShare.Core.Abstractions.RequestHandling
{
    public interface IErrorHandler
    {
        void CreateUserTokenFailed(string message, Exception error, CreateUserToken request);
        void UpdateUserPasswordFailed(string message, Exception error, UpdateUserPassword request);
    }
}
