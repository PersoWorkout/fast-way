using Domain.DTOs.Users.Request;
using Domain.Errors;
using Domain.ValueObjects;
using FluentValidation;

namespace Application.Validators.Users
{
    public class UpdateUserRequestValidator: AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator() 
        {
            RuleFor(x =>  x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithErrorCode(EmailErrors.Invalid.Code)
                .WithMessage(EmailErrors.Invalid.Description);

            RuleFor(x => x.Password)
                .Must(x => PasswordValueObject.Create(x).IsSucess)
                .When(x => !string.IsNullOrEmpty(x.Password))
                .WithErrorCode(PasswordErrors.Invalid.Code)
                .WithMessage(PasswordErrors.Invalid.Description);
        }
    }
}