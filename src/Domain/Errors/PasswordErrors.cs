using Domain.Abstractions;

namespace Domain.Errors;

public static class PasswordErrors
{
    public static readonly Error Empty = new("Password.Empty", "'Password' must be not empty");
    public static readonly Error Invalid = new("Password.Invalid", "'Password' is not a valid password");
    public static readonly Error InvalidConfirmation = new("PasswordConfirmation.NotEqual", "'Password' and 'PasswordConfirmation' must be equal");
}
