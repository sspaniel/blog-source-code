using AdventureShare.Core.Models.Internal;
using System;
using System.Linq;

namespace AdventureShare.Core.Helpers
{
    public static class FluentExtensions
    {
        public static Tuple<ValidationResult, Tin> Validate<Tin>(this ValidationResult validationResult, Tin input)
        {
            return new Tuple<ValidationResult, Tin>(validationResult, input);
        }

        public static Tuple<ValidationResult, bool> If<Tin>(this Tuple<ValidationResult, Tin> previous, params Func<Tin, bool>[] predicates)
        {
            var isTrue = predicates.Any(x => x(previous.Item2));
            return new Tuple<ValidationResult, bool>(previous.Item1, isTrue);
        }

        public static void AddError(this Tuple<ValidationResult, bool> previous, string error)
        {
            if (previous.Item2)
            {
                previous.Item1.Errors.Add(error);
            }
        }
    }
}
