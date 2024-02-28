using Application.Commands.Authorization;
using Application.Interfaces;
using Application.Services.Authorization;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Authorization;
using Domain.Errors;
using Domain.Models;
using Domain.ValueObjects;
using MediatR;

namespace Application.Handlers.Authorization
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<ConnectedResponse>>
    {
        private readonly IAuthorizationRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public RegisterHandler(IAuthorizationRepository authRepository, IUserRepository userRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<ConnectedResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.EmailAlreadyUsed(request.Email.Value))
                return Result<ConnectedResponse>.Failure(EmailErrors.Invalid);

            string hashedPassword = HashService.Hash(password: request.Password.Value)!;

            var user = _mapper.Map<User>(request);

            user.Password = PasswordValueObject.Create(hashedPassword).Data!;

            user = await _userRepository.Create(user);

            var token = TokenService.Generate();
            var hashedToken = HashService.Hash(token)!;

            var session = await _authRepository.CreateSession(user.Id, hashedToken);

            session.Token = token;

            return Result<ConnectedResponse>.Success(
                _mapper.Map<ConnectedResponse>(session));
        }
    }
}
