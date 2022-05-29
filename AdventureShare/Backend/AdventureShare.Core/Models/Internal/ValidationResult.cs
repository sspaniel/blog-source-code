using System.Collections.Generic;
using System.Linq;

namespace AdventureShare.Core.Models.Internal
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new LinkedList<string>();
        }

        public bool IsValid => !Errors.Any();
        public ICollection<string> Errors { get; set; }
    }
}
