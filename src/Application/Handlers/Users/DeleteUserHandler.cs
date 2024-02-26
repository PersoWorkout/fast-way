using Application.Commands.Users;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Errors;
using MediatR;

namespace Application.Handlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<object>>
    {
        private readonly IUserRepository _userRepository;
        
        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id);
            if (user is null)
                return Result<object>.Failure(
                    UserErrors.NotFound(
                        request.Id.ToString()));

            await _userRepository.Delete(user);

            return Result<object>.Success();
        }
    }
}
