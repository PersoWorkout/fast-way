using Application.Interfaces;
using Domain.DTOs.Users.Request;
using Domain.Errors;
using Domain.ValueObjects;
using FluentValidation;

namespace Application.Validators.Users
{
    public class CreateUserRequestValidator: AbstractValidator<CreateUserRequest>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserRequestValidator(IUserRepository userRepository) {

            _userRepository = userRepository;

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
                .EmailAddress();
            //.MustAsync(async (email, _) => 
            //    !await _userRepository
            //        .EmailAlreadyUsed(email))
            //.WithErrorCode(EmailErrors.Invalid.Code)
            //.WithMessage(EmailErrors.Invalid.Description);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithErrorCode(PasswordErrors.Empty.Code)
                .WithMessage(PasswordErrors.Empty.Description);
                //.Must((password) => 
                //    PasswordValueObject
                //        .Create(password).IsSucess)
                //.WithErrorCode(PasswordErrors.Invalid.Code)
                //.WithMessage(PasswordErrors.Invalid.Description);
        }
    }
}
