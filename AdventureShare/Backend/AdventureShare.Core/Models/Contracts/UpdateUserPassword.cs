namespace AdventureShare.Core.Models.Contracts
{
    public class UpdateUserPassword
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
