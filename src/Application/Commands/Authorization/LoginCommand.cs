using Domain.Abstractions;
using Domain.Models;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands.Authorization
{
    public class LoginCommand: IRequest<Result<Session>>
    {
        public required EmailValueObject Email {  get; set; }
        public required PasswordValueObject Password { get; set; }
    }
}
