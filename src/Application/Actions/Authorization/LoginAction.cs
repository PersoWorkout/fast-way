using Application.Commands.Authorization;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Authorization;
using Domain.DTOs.Authorization.Requests;
using FluentValidation;
using MediatR;

namespace Application.Actions.Authorization
{
    public class LoginAction
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginRequest> _validator;

        public LoginAction(ISender sender, IMapper mapper, IValidator<LoginRequest> validator)
        {
            _sender = sender;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<ConnectedResponse>> Execute(LoginRequest request)
        {
            var requestValidation = _validator.Validate(request);
            if (!requestValidation.IsValid)
            {
                return Result<ConnectedResponse>.Failure(
                    requestValidation.Errors.Select(          
                        error => new Error(error.ErrorCode, error.ErrorMessage))
                    .ToList());
            }

            return await _sender.Send(
                _mapper.Map<LoginCommand>(request));
        }
    }
}
