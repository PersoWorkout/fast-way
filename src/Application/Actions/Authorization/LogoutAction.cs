using Application.Commands.Authorization;
using Domain.Abstractions;
using MediatR;

namespace Application.Actions.Authorization
{
    public class LogoutAction
    {
        private readonly ISender _sender;

        public LogoutAction(ISender sender)
        {
            _sender = sender;
        }

        public async Task<Result<object>> Execute(string token)
        {
            return await _sender.Send(new LogoutCommand { Token = token });
        }
    }
}
