using Application.Commands.Users;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Users.Request;
using Domain.DTOs.Users.Response;
using Domain.Errors;
using FluentValidation;
using MediatR;

namespace Application.Actions.Users
{
    public class UpdateUserAction(
        ISender sender,
        IMapper mapper,
        IValidator<UpdateUserRequest> validator)
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<UpdateUserRequest> _validator = validator;

        public async Task<Result<UserDetails>> Execute(string userId, UpdateUserRequest request)
        {
            if (!Guid.TryParse(userId, out var parsedId))
            {
                return Result<UserDetails>.Failure(
                    UserErrors.NotFound(userId));
            }

            var requestValidation = _validator.Validate(request);
            if (!requestValidation.IsValid)
                return Result<UserDetails>
                    .Failure(requestValidation.Errors
                        .Select(x => 
                            new Error(x.ErrorCode, x.ErrorMessage))
                        .ToList());

            var command = _mapper.Map<UpdateUserCommand>(request);
            command.Id = parsedId;

            return await _sender.Send(command);
        }
    }
}
