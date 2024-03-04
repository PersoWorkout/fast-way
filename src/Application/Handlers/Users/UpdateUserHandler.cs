using Application.Commands.Users;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Errors;
using Domain.Models;
using MediatR;
using System.Net;

namespace Application.Handlers.Users
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Email is not null && 
                await _userRepository.EmailAlreadyUsed(request.Email))
                return Result<User>.Failure(
                    EmailErrors.Invalid);

            var user = await _userRepository.GetById(request.Id);
            if(user is null)
                return Result<User>
                    .Failure(UserErrors.NotFound(
                        request.Id.ToString()),
                    HttpStatusCode.NotFound);

            user.Update(
                request.Firstname,
                request.Lastname,
                request.Email,
                request.Password);

            user = await _userRepository.Update(user);

            return Result<User>.Success(user);
        }
    }
}
