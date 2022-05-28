using AdventureShare.Core.Abstractions;
using AdventureShare.Core.Helpers;
using AdventureShare.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureShare.Core.Implementations
{
    public class InMemoryRepository : IRepository
    {
        private readonly ICollection<User> _users = new List<User>
        {
            new User
            {
                UserId = 1,
                Email = "test@website.com",
                DisplayName = "Test User",
                CreatedUtc = DateTime.Parse("1/1/2000")
            }
        };

        private readonly ICollection<UserLogin> _userLogins = new List<UserLogin>
        {
            new UserLogin
            {
                UserId = 1,
                Email = "test@website.com",
                DisplayName = "Test User",
                PasswordHash = PasswordHasher.Hash("testpw", DateTime.Parse("1/1/2000").ToString()),
                CreatedUtc = DateTime.Parse("1/1/2000")
            }
        };

        private readonly ICollection<Permission> _permissions = new List<Permission>
        {
            new Permission
            {
                PermissionId = 1,
                Name = "Test Permission 1"
            },
            new Permission
            {
                PermissionId = 2,
                Name = "Test Permission 2"
            },
            new Permission
            {
                PermissionId = 3,
                Name = "Test Permission 3"
            }
        };

        private readonly ICollection<UserPermission> _userPermissions = new List<UserPermission>
        {
            new UserPermission
            {
                UserId = 1,
                PermissionId = 2
            },
            new UserPermission
            {
                UserId = 1,
                PermissionId = 2
            },
            new UserPermission
            {
                UserId = 1,
                PermissionId = 2
            }
        };

        public Task<UserLogin> GetUserLoginAsync(string email)
        {
            var userLogin = _userLogins
                .FirstOrDefault(x => x.Email == email);

            return Task.FromResult(userLogin);
        }

        public Task<UserLogin> GetUserLoginAsync(int userId)
        {
            var userLogin = _userLogins
                .FirstOrDefault(x => x.UserId == userId);

            return Task.FromResult(userLogin);
        }

        public Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId)
        {
            var userPermissions = _userPermissions
                .Where(x => x.UserId == userId);

            var permissions = _permissions
                .Where(x => userPermissions.Any(y => y.PermissionId == x.PermissionId));

            return Task.FromResult(permissions);
        }

        public async Task UpdateUserLoginAsync(UserLogin userLogin)
        {
            var existingUserLogin = await GetUserLoginAsync(userLogin.UserId);
            existingUserLogin.PasswordHash = userLogin.PasswordHash;
        }
    }
}
