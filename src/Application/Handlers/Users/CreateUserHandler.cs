﻿using Application.Commands.Users;
using Application.Interfaces;
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

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserForList>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            List<Error> errors = [];

            var emailValue = EmailValueObject.Create(request.Email);
            if (emailValue.IsFailure)
                errors.Add(EmailErrors.Invalid);

            var passwordValue = PasswordValueObject.Create(request.Password);
            if(passwordValue.IsFailure)
                errors.Add(PasswordErrors.Invalid);

            if(errors.Count > 0)
                return Result<UserForList>.Failure(errors);

            if (await _userRepository.EmailAlreadyUsed(emailValue.Data!.Value))
                return Result<UserForList>
                    .Failure(EmailErrors.Invalid);

            var user = await _userRepository.Create(
                _mapper.Map<User>(request));

            return Result<UserForList>.Success(
                _mapper.Map<UserForList>(user));
        }
    }
}
