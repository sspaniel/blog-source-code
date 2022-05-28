namespace AdventureShare.Core.Models.Contracts
{
    public class UserToken
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string ExpiresUtc { get; set; }
        public string TokenValue { get; set; }
    }
}
