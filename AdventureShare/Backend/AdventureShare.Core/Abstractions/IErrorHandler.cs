using AdventureShare.Core.Models.Contracts;
using System;
using System.Threading.Tasks;

namespace AdventureShare.Core.Abstractions
{
    public interface IErrorHandler
    {
        Task UserLoginFailed(Exception error, UserLoginRequest request);
    }
}
