using System.Collections.Generic;
using System.Linq;

namespace AdventureShare.Core.Models.Common
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = Enumerable.Empty<string>();
        }

        public bool IsValid => !Errors.Any();
        public IEnumerable<string> Errors { get; set; }
    }
}
