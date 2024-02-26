using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands.Users
{
    public class UpdateUserCommand: IRequest<Result<UserDetails>> 
    {
        public Guid Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public EmailValueObject? Email { get; set; }
        public PasswordValueObject? Password { get; set; }
    }
}
