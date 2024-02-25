using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using MediatR;

namespace Application.Queries
{
    public class GetUserByIdQuery(Guid id) : 
        IRequest<Result<UserDetails>>
    {
        public Guid Id { get; set; } = id;
    }
}
