using AdventureShare.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureShare.Core.Abstractions
{
    public interface IRepository
    {
        Task<UserLogin> GetUserLoginAsync(string email);
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(Guid id);
        Task UpdateUserLoginAsync(UserLogin userLogin);
    }
}
