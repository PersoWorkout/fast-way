using Application.Interfaces;
using Application.Queries;
using Domain.Abstractions;
using Domain.Errors;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id);
            if (user is null)
                return Result<User>.Failure(
                    UserErrors.NotFound(request.Id.ToString()));

            return Result<User>.Success(user);
        }
    }
}
