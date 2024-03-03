using Application.Commands.Users;
using Application.Interfaces;
using Application.Services.Authorization;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using Domain.Errors;
using Domain.Models;
using Domain.ValueObjects;
using MediatR;

namespace Application.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<UserForList>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly HashService _hashService;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper, HashService hashServicve)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _hashService = hashServicve;
        }

        public async Task<Result<UserForList>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.EmailAlreadyUsed(request.Email))
                return Result<UserForList>
                    .Failure(EmailErrors.Invalid);

            string hashedPassword = _hashService.Hash(password: request.Password.Value)!;

            var user = _mapper.Map<User>(request);

            user.Password = PasswordValueObject.Create(hashedPassword).Data!;

            user = await _userRepository.Create(
                _mapper.Map<User>(request));

            return Result<UserForList>.Success(
                _mapper.Map<UserForList>(user));
        }
    }
}
