using AdventureShare.Core.Models.Internal;

namespace AdventureShare.Core.Helpers
{
    public static class Is
    {
        public static bool Null<Tin>(Tin input) => input is null;
        public static bool Default<Tin>(Tin input) => input?.Equals(default) ?? true;
        public static bool NullOrWhiteSpace(string input) => string.IsNullOrWhiteSpace(input);
        public static bool LessThan0(int? input) => input < 0;
        public static bool Invalid(ValidationResult validationResult) => !validationResult.IsValid;
    }
}
