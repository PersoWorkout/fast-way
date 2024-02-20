using Domain.Abstractions;

namespace Domain.Errors;

public sealed class UserErrors
{
    public static Error InvalidPayload = new("User.InvalidPayload", "The payload is not valid");
    public static Error NotFound(string id) => new("User.NotFound", $"The user with id = {id} was not found");
}

