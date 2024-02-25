using Domain.Abstractions;

namespace Domain.Errors;

public static class EmailErrors
{
    public static readonly Error Empty = new("Email.Empty", "'Email' must not be empty");
    public static readonly Error Invalid = new("Email.Invalid", "'Email' is not a valid email address");
}

