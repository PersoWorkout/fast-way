using Domain.Abstractions;

namespace Domain.Errors;

public static class EmailErrors
{
    public static Error IsRequired = new("Email.IsRequired", "The email value is required");
    public static Error InvalidEmail = new("Email.Invalid", "The value is not a valid email");
}

