using Domain.Abstractions;
using Domain.Errors;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class EmailValueObject : ValueObject
    {
        private const string RegexPatternValue = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        private const RegexOptions RegexOptionsValue = RegexOptions.IgnoreCase;

        public string Value { get; set; }

        private EmailValueObject(string value)
        {
            Value = value;
        }

        public static Result<EmailValueObject> Create(string value)
        {
            var errors = new List<Error>();

            if (string.IsNullOrEmpty(value))
                errors.Add(EmailErrors.Empty);

            if (!Regex.IsMatch(
                    value,
                    RegexPatternValue,
                    RegexOptionsValue,
                    TimeSpan.FromMilliseconds(250)))
                errors.Add(EmailErrors.Invalid);

            if (errors.Count > 0)
                return Result<EmailValueObject>.Failure(errors);

            return Result<EmailValueObject>.Success(new EmailValueObject(value));

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
