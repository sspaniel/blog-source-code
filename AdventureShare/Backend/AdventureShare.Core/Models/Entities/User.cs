using Dapper.Contrib.Extensions;
using System;

namespace AdventureShare.Core.Models.Entities
{
    [Table("[dbo].[User]")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public DateTime? LastLoginUtc { get; set; }
        public DateTime? LastUpdateUtc { get; set; }
        public DateTime CreatedUtc { get; set; }
    }
}
