using Application.Commands.Authorization;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Authorization;
using Domain.DTOs.Authorization.Requests;
using FluentValidation;
using MediatR;

namespace Application.Actions.Authorization
{
    public class RegisterAction
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterRequest> _validator;

        public RegisterAction(ISender sender, IMapper mapper, IValidator<RegisterRequest> validator)
        {
            _sender = sender;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<ConnectedResponse>> Execute(RegisterRequest request)
        {
            var requestValidation = _validator.Validate(request);
            if(!requestValidation.IsValid) 
            {
                return Result<ConnectedResponse>.Failure(
                    requestValidation.Errors.Select(
                        x => new Error(x.ErrorCode, x.ErrorMessage))
                    .ToList());
            }

            return await _sender.Send(
                _mapper.Map<RegisterCommand>(request));
        }
    }
}
