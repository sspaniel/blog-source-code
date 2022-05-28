using AdventureShare.Core.Models.Contracts;
using AdventureShare.Core.Models.Internal;

namespace AdventureShare.Core.Abstractions
{
    public interface IValidator
    {
        ValidationResult Validate(CreateUserToken request);
        ValidationResult Validate(UpdateUserPassword request);
    }
}
