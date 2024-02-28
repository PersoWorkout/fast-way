using Domain.Abstractions;
using MediatR;

namespace Application.Commands.Authorization
{
    public class LogoutCommand: IRequest<Result<object>>
    {
        public required string Token { get; set; }
    }
}
