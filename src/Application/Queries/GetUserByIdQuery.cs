using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Queries
{
    public class GetUserByIdQuery(Guid id) : 
        IRequest<Result<User>>
    {
        public Guid Id { get; set; } = id;
    }
}
