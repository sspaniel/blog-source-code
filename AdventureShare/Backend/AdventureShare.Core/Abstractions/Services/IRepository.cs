using AdventureShare.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureShare.Core.Abstractions.Services
{
    public interface IRepository
    {
        Task<UserLogin> GetUserLoginAsync(string email);
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId);
        Task UpdateUserLoginAsync(UserLogin userLogin);
        Task<UserLogin> GetUserLoginAsync(int userId);
    }
}
