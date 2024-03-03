using Application.Commands.Users;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Errors;
using MediatR;
using System.Net;

namespace Application.Handlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<object>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;

        public DeleteUserHandler(IUserRepository userRepository, IAuthorizationRepository authorizationRepository)
        {
            _userRepository = userRepository;
            _authorizationRepository = authorizationRepository;
        }

        public async Task<Result<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id);
            if (user is null)
                return Result<object>.Failure(
                    UserErrors.NotFound(
                        request.Id.ToString()),
                    HttpStatusCode.NotFound);

            await _authorizationRepository.DestroyByUser(user.Id);

            await _userRepository.Delete(user);

            return Result<object>.Success();
        }
    }
}
