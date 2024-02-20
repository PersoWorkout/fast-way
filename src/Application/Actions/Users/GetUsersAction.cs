using Application.Queries;
using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Actions.Users
{
    public class GetUsersAction(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;

        public async Task<Result<List<User>>> Execute()
        {
            return await _mediator.Send(new GetUsersQuery());
        }
    }
}
