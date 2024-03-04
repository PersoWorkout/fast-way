using Domain.Abstractions;
using Domain.Enums;
using Domain.Models;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands.Users
{
    public class CreateUserCommand: IRequest<Result<User>>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public EmailValueObject Email { get; set; }
        public PasswordValueObject Password { get; set; }
        public UserRoles Roles { get; set; }
    }
}
