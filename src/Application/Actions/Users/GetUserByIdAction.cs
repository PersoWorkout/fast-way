using Application.Queries;
using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using Domain.Errors;
using Domain.Models;
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

        public async Task<Result<User>> Execute(string id)
        {
            if(!Guid.TryParse(id, out var parsedId))
            {
                return Result<User>.Failure(
                    UserErrors.NotFound(id),
                    HttpStatusCode.NotFound);
            }
            return await _sender.Send(
                new GetUserByIdQuery(parsedId));
        }
    }
}
