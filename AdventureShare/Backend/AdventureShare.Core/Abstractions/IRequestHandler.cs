using AdventureShare.Core.Models.Contracts;
using System.Threading.Tasks;

namespace AdventureShare.Core.Abstractions
{
    public interface IRequestHandler
    {
        Task<Response<UserToken>> CreateUserTokenAsync(CreateUserToken request);

        Task<Response<string>> UpdateUserPasswordAsync(UpdateUserPassword request, UserToken userToken);
    }
}
