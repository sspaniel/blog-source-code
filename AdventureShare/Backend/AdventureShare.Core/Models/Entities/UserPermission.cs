using Dapper.Contrib.Extensions;

namespace AdventureShare.Core.Models.Entities
{
    [Table("[dbo].[UserPermission]")]
    public class UserPermission
    {
        [ExplicitKey]
        public int UserId { get; set; }

        [ExplicitKey]
        public int PermissionId { get; set; }
    }
}
