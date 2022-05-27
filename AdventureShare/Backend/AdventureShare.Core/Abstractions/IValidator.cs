using AdventureShare.Core.Models.Common;
using AdventureShare.Core.Models.Contracts;

namespace AdventureShare.Core.Abstractions
{
    public interface IValidator
    {
        ValidationResult Validate(UserLoginRequest request);
    }
}
