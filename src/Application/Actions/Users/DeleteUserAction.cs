using Application.Commands.Users;
using Domain.Abstractions;
using Domain.Errors;
using MediatR;

namespace Application.Actions.Users
{
    public class DeleteUserAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<object>> Execute(string id)
        {
            if(!Guid.TryParse(id, out var parsedId))
                return Result<object>.Failure(
                    UserErrors.NotFound(id));

            return await _sender.Send(
                new DeleteUserCommand(parsedId));
        }
    }
}
