using Domain.Abstractions;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands.Authorization
{
    public class RegisterCommand: IRequest<Result<object>>
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required EmailValueObject Email { get; set; }
        public required PasswordValueObject Password { get; set; }
    }
}
