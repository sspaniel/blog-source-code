using Dapper.Contrib.Extensions;
using System;

namespace AdventureShare.Core.Models.Entities
{
    [Table("[dbo].[Permission]")]
    public class Permission
    {
        [ExplicitKey]
        public Guid PermissionId { get; set; }
        public string Name { get; set; }
    }
}
