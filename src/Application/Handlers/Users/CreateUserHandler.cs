using Application.Commands.Users;
using Application.Interfaces;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using Domain.Errors;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<UserForList>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserForList>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.EmailAlreadyUsed(request.Email.Value))
                return Result<UserForList>
                    .Failure(EmailErrors.Invalid);

            var user = await _userRepository.Create(
                _mapper.Map<User>(request));

            return Result<UserForList>.Success(
                _mapper.Map<UserForList>(user));
        }
    }
}
