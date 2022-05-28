using AdventureShare.Core.Abstractions;
using AdventureShare.Core.Models.Contracts;
using AdventureShare.Core.Models.Internal;
using System.Collections.Generic;

namespace AdventureShare.Core.Implementations
{
    public class GlobalValidator : IValidator
    {
        public ValidationResult Validate(CreateUserToken request)
        {
            var validationErrors = new List<string>();

            if (string.IsNullOrWhiteSpace(request?.Email))
            {
                validationErrors.Add($"{nameof(request.Email)} is required");
            }

            if (string.IsNullOrWhiteSpace(request?.Password))
            {
                validationErrors.Add($"{nameof(request.Password)} is required");
            }

            var result = new ValidationResult { Errors = validationErrors };
            return result;
        }

        public ValidationResult Validate(UpdateUserPassword request)
        {
            var validationErrors = new List<string>();

            if (request?.UserId == default)
            {
                validationErrors.Add($"{nameof(request.UserId)} is required");
            }

            if (string.IsNullOrWhiteSpace(request?.NewPassword))
            {
                validationErrors.Add($"{nameof(request.NewPassword)} is required");
            }

            // TODO add stronger password rules

            var result = new ValidationResult { Errors = validationErrors };
            return result;
        }
    }
}
