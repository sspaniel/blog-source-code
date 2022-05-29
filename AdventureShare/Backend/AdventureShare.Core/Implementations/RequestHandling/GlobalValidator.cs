using AdventureShare.Core.Abstractions.RequestHandling;
using AdventureShare.Core.Helpers;
using AdventureShare.Core.Models.Contracts;
using AdventureShare.Core.Models.Internal;

namespace AdventureShare.Core.Implementations.RequestHandling
{
    public class GlobalValidator : IValidator
    {
        public ValidationResult Validate(CreateUserToken request)
        {
            var result = new ValidationResult();

            result
                .Validate(request?.Email)
                .If(Is.NullOrWhiteSpace)
                .AddError($"{nameof(request.Email)} is required");

            result
                .Validate(request?.Password)
                .If(Is.NullOrWhiteSpace)
                .AddError($"{nameof(request.Password)} is required");

            return result;
        }

        public ValidationResult Validate(UpdateUserPassword request)
        {
            var result = new ValidationResult();

            result
                .Validate(request?.UserId)
                .If(Is.Default, Is.LessThan0)
                .AddError($"{nameof(request.UserId)} is required");

            result
                .Validate(request?.NewPassword)
                .If(Is.NullOrWhiteSpace)
                .AddError($"{nameof(request.NewPassword)} is required");

            // TODO add stronger password rules

            return result;
        }
    }
}
