using Application.Commands.Users;
using Application.Interfaces;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using Domain.Errors;
using MediatR;

namespace Application.Handlers.Users
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<UserDetails>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDetails>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Email is not null && 
                await _userRepository.EmailAlreadyUsed(request.Email))
                return Result<UserDetails>.Failure(
                    EmailErrors.Invalid);

            var user = await _userRepository.GetById(request.Id);
            if(user is null)
                return Result<UserDetails>
                    .Failure(UserErrors.NotFound(
                        request.Id.ToString()));

            if (!string.IsNullOrEmpty(request.Firstname)) 
                user.Firstname = request.Firstname;

            if (!string.IsNullOrEmpty(request.Lastname))
                user.Lastname = request.Lastname;

            if (request.Email is not null)
                user.Email = request.Email;

            if (request.Password is not null)
                user.Password = request.Password;

            user = await _userRepository.Update(user);

            return Result<UserDetails>.Success(
                _mapper.Map<UserDetails>(user));
        }
    }
}
