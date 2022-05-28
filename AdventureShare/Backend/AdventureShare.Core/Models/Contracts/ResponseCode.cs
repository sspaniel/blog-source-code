namespace AdventureShare.Core.Models.Contracts
{
    public enum ResponseCode
    {
        Success,
        AuthenticationFailed,
        ValidationFailed,
        AuthorizationFailed,
        InternalError
    }
}