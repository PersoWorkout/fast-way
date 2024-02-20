using Domain.Abstractions;
using Domain.Errors;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public class PasswordValueObject : ValueObject
{
    private const string PasswordRegexPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[~`!@#\$%\^&\*\(\)_\-\+=\{\[\}\]\|\\:;'<,>\.\?\/]).{8,}$";
    private const RegexOptions PasswordRegexOptions = RegexOptions.None;
    public string Value { get; set; }

    private PasswordValueObject(string value) 
    {
        Value = value;
    }

    public static Result<PasswordValueObject> Create(string value)
    {
        List<Error> errors = [];
        if (string.IsNullOrEmpty(value))
            errors.Add(PasswordErrors.IsRequired);

        if (!Regex.IsMatch(value,
                PasswordRegexPattern,
                PasswordRegexOptions,
                TimeSpan.FromMilliseconds(250)))
            errors.Add(PasswordErrors.IsInvalid);

        if (errors.Count > 0)
            return Result<PasswordValueObject>.Failure(errors);

        return Result<PasswordValueObject>
            .Success(new PasswordValueObject(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
