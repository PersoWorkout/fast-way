using Application.Commands.Authorization;
using Application.Interfaces;
using Application.Services.Authorization;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Authorization;
using Domain.Errors;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Authorization
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<ConnectedResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IMapper _mapper;


        public LoginHandler(IUserRepository userRepository, IAuthorizationRepository authorizationRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _authorizationRepository = authorizationRepository;
            _mapper = mapper;
        }

        public async Task<Result<ConnectedResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);
            if(user is null)
            {
                return Result<ConnectedResponse>
                    .Failure(AuthErrors.InvalidCreadentials);
            }

            if(!HashService.Verify(request.Password.Value, user.Password.Value))
            {
                return Result<ConnectedResponse>
                    .Failure(AuthErrors.InvalidCreadentials);
            }

            var token = TokenService.Generate();
            var hashedToken = HashService.Hash(token)!;

            var session = await _authorizationRepository.CreateSession(
                new Session(
                    user.Id, 
                    hashedToken));

            session.Token = token;

            return Result<ConnectedResponse>.Success(_mapper.Map<ConnectedResponse>(session));

            throw new NotImplementedException();
        }
    }
}
