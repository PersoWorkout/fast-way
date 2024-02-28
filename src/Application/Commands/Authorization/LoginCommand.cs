using Domain.Abstractions;
using Domain.DTOs.Authorization;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands.Authorization
{
    public class LoginCommand: IRequest<Result<ConnectedResponse>>
    {
        public required EmailValueObject Email {  get; set; }
        public required PasswordValueObject Password { get; set; }
    }
}
