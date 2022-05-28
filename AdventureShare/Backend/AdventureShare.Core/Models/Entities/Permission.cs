using Dapper.Contrib.Extensions;

namespace AdventureShare.Core.Models.Entities
{
    [Table("[dbo].[Permission]")]
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        public string Name { get; set; }
    }
}
