using Domain.Abstractions;
using MediatR;

namespace Application.Commands.Users
{
    public class DeleteUserCommand: IRequest<Result<object>>
    {
        public Guid Id { get; set; }

        public DeleteUserCommand(Guid id) => Id = id;
    }
}
