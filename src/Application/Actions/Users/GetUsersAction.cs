using Application.Queries;
using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using MediatR;

namespace Application.Actions.Users
{
    public class GetUsersAction(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;

        public async Task<Result<List<UserForList>>> Execute()
        {
            return await _mediator.Send(new GetUsersQuery());
        }
    }
}
