using Application.Queries;
using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using Domain.Errors;
using MediatR;
using System.Net;

namespace Application.Actions.Users
{
    public class GetUserByIdAction
    {
        private readonly ISender _sender;

        public GetUserByIdAction(ISender sender)
        {
            _sender = sender;
        }

        public async Task<Result<UserDetails>> Execute(string id)
        {
            if(!Guid.TryParse(id, out var parsedId))
            {
                return Result<UserDetails>.Failure(
                    UserErrors.NotFound(id),
                    HttpStatusCode.NotFound);
            }
            return await _sender.Send(
                new GetUserByIdQuery(parsedId));
        }
    }
}
