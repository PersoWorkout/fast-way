using Domain.Abstractions;

namespace Domain.Errors;

public static class PasswordErrors
{
    public static Error IsRequired = new("Password.IsRequired", "The password is required");
    public static Error IsInvalid = new("Password.Invalid", "The value is not a valid password");
}
