using Application.Queries;
using Domain.Abstractions;
using Domain.Errors;
using Domain.Models;
using MediatR;

namespace Application.Actions.Users
{
    public class GetUserByIdAction
    {
        private readonly ISender _sender;

        public GetUserByIdAction(ISender sender)
        {
            _sender = sender;
        }

        public async Task<Result<User>> Execute(string id)
        {
            if(!Guid.TryParse(id, out var parsedId))
            {
                return Result<User>.Failure(
                    UserErrors.NotFound(id));
            }
            return await _sender.Send(
                new GetUserByIdQuery(parsedId));
        }
    }
}
