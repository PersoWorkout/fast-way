using Application.Commands.Users;
using Domain.Abstractions;
using Domain.Errors;
using MediatR;
using System.Net;

namespace Application.Actions.Users
{
    public class DeleteUserAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<object>> Execute(string id)
        {
            if(!Guid.TryParse(id, out var parsedId))
                return Result<object>.Failure(
                    UserErrors.NotFound(id),
                    HttpStatusCode.NotFound);

            return await _sender.Send(
                new DeleteUserCommand(parsedId));
        }
    }
}
