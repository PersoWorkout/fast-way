using Domain.DTOs.Authorization.Requests;
using Domain.Errors;
using Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Authorization
{
    public class RegisterRequestValidation: AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidation() {
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

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password)
                .WithErrorCode(PasswordErrors.InvalidConfirmation.Code)
                .WithMessage(PasswordErrors.InvalidConfirmation.Description);

        }
    }
}
