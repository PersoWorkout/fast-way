using Application.Commands.Authorization;
using Application.Interfaces;
using Application.Services.Authorization;
using Domain.Abstractions;
using Domain.Errors;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Authorization
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<Session>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly HashService _hashService;

        public LoginHandler(IUserRepository userRepository, IAuthorizationRepository authorizationRepository, HashService hashService)
        {
            _userRepository = userRepository;
            _authorizationRepository = authorizationRepository;
            _hashService = hashService;
        }

        public async Task<Result<Session>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);
            if(user is null)
            {
                return Result<Session>
                    .Failure(AuthErrors.InvalidCreadentials);
            }

            if(!_hashService.Verify(request.Password.Value, user.Password.Value))
            {
                return Result<Session>
                    .Failure(AuthErrors.InvalidCreadentials);
            }

            var token = TokenService.Generate();
            var hashedToken = _hashService.Hash(token)!;

            var session = await _authorizationRepository.CreateSession(
                new Session(
                    user.Id, 
                    hashedToken));

            session.Token = token;

            return Result<Session>.Success(session);
        }
    }
}
