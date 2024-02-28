using Domain.DTOs.Authorization.Requests;
using Domain.Errors;
using Domain.ValueObjects;
using FluentValidation;

namespace Application.Validators.Authorization
{
    public class LoginRequestValidation: AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation() 
        {
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
        }
    }
}
