using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using MediatR;

namespace Application.Commands.Users
{
    public class CreateUserCommand: IRequest<Result<UserForList>>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
