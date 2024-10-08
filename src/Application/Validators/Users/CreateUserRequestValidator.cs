﻿using Domain.DTOs.Users.Request;
using Domain.Enums;
using Domain.Errors;
using Domain.ValueObjects;
using FluentValidation;

namespace Application.Validators.Users
{
    public class CreateUserRequestValidator: AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator() 
        {

            RuleFor(x => x.Firstname)
                .NotEmpty()
                .WithErrorCode("Firstname.Empty")
                .WithMessage("'Firstname' must not be empty");

            RuleFor(x => x.Lastname)
                .NotEmpty()
                .WithErrorCode("Lastname.Empty")
                .WithMessage("'Lastname' must not be empty");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithErrorCode(EmailErrors.Empty.Code)
                .WithMessage(EmailErrors.Empty.Description)
                .EmailAddress()
                .WithErrorCode(EmailErrors.Invalid.Code)
                .WithMessage(EmailErrors.Invalid.Description);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithErrorCode(PasswordErrors.Empty.Code)
                .WithMessage(PasswordErrors.Empty.Description)
                .Must((password) =>
                    PasswordValueObject
                        .Create(password).IsSucess)
                .WithErrorCode(PasswordErrors.Invalid.Code)
                .WithMessage(PasswordErrors.Invalid.Description);

            RuleFor(x => x.Role)
                .NotNull()
                .WithErrorCode("Role.Empty")
                .WithMessage("'Role' must not be empty.")
                .Must(x =>
                    Enum.IsDefined(typeof(UserRoles), x))
                .WithErrorCode("Role.Invalid")
                .WithMessage("'Role' is not a valid user roles.");
        }
    }
}
