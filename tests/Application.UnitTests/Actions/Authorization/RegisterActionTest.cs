using Application.Actions.Authorization;
using Application.Commands.Authorization;
using Application.UnitTests.Configuration.Mappers;
using Application.Validators.Authorization;
using Domain.DTOs.Authorization.Requests;
using Domain.Errors;
using MediatR;
using Moq;

namespace Application.UnitTests.Actions.Authorization
{
    public class RegisterActionTest
    {
        private readonly Mock<ISender> _mockedSender;
        private readonly RegisterAction _action;

        public RegisterActionTest()
        {
            _mockedSender = new Mock<ISender>();

            _action = new(_mockedSender.Object,
                MapperConfigurator.CreateMapperForAuthProfile(),
                new RegisterRequestValidation());
        }

        [Fact]
        public async Task Execute_ShouldReturnFailure_WhenRequestIsNotValid()
        {
            //Assert
            var request = new RegisterRequest()
            {
                Firstname = "John",
                Lastname = "",
                Email = "john.doe@example.com",
                Password = "Password",
                PasswordConfirmation = "Password123!"
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(PasswordErrors.Invalid, result.Errors);
            Assert.Contains(PasswordErrors.InvalidConfirmation, result.Errors);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccessResult_WhenPayloadIsValid()
        {
            //Assert
            var request = new RegisterRequest()
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!",
                PasswordConfirmation = "Password123!"
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            _mockedSender.Verify(
                x => x.Send(It.IsAny<RegisterCommand>(), default),
                Times.Once());
        }
    }
}
