using Application.Commands.Users;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Users.Request;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Actions.Users
{
    public class CreateUserAction
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserRequest> _validator;

        public CreateUserAction(
            ISender sender, 
            IMapper mapper, 
            IValidator<CreateUserRequest> validator)
        {
            _sender = sender;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<User>> Execute(CreateUserRequest request)
        {
            List<Error> errors = [];

            var requestValidation = _validator.Validate(request);
            if (!requestValidation.IsValid)
            {
             return Result<User>.Failure(
                    requestValidation.Errors
                        .Select(x => new Error(
                            x.ErrorCode, 
                            x.ErrorMessage))
                        .ToList());
            }

            return await _sender.Send(
                _mapper.Map<CreateUserCommand>(request));
        }
    }
}
