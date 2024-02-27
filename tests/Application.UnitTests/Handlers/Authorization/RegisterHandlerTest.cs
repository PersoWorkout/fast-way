using Application.Commands.Authorization;
using Application.Handlers.Authorization;
using Application.Interfaces;
using Application.UnitTests.Configuration.Mappers;
using Domain.Errors;
using Domain.Models;
using Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Handlers.Authorization
{
    public class RegisterHandlerTest
    {
        private readonly Mock<IAuthorizationRepository> _mockedAuthRepository;
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly RegisterHandler _handler;

        public RegisterHandlerTest()
        {
            _mockedAuthRepository = new Mock<IAuthorizationRepository>();
            _mockedUserRepository = new Mock<IUserRepository>();

            _handler = new(_mockedAuthRepository.Object,
                _mockedUserRepository.Object,
                MapperConfigurator.CreateMapperForAuthProfile());
        }

        [Fact]
        public async Task Handle_ReturnFailureResult_WhenEmailAlreadyUsed()
        {
            //Arrange
            var command = new RegisterCommand()
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = EmailValueObject.Create("john.doe@example.com").Data!,
                Password = PasswordValueObject.Create("Password123!").Data!
            };

            _mockedUserRepository.Setup(
                x => x.EmailAlreadyUsed(command.Email.Value))
                .ReturnsAsync(true);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(EmailErrors.Invalid, result.Errors);

            _mockedUserRepository.Verify(
                x => x.Create(It.IsAny<User>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenUserWasCreated()
        {
            //Arrange
            var command = new RegisterCommand()
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = EmailValueObject.Create("john.doe@example.com").Data!,
                Password = PasswordValueObject.Create("Password123!").Data!
            };

            _mockedUserRepository.Setup(
                x => x.EmailAlreadyUsed(command.Email.Value))
                .ReturnsAsync(false);

            _mockedUserRepository.Setup(
                x => x.Create(It.IsAny<User>()))
                .ReturnsAsync(new User
                {
                    Firstname = command.Firstname, 
                    Lastname = command.Lastname, 
                    Email = command.Email, 
                    Password = command.Password
                });

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);

            _mockedUserRepository.Verify(
                x => x.Create(It.IsAny<User>()),
                Times.Once);

            _mockedAuthRepository.Verify(
                x => x.CreateSession(It.IsAny<Guid>(), It.IsAny<string>()),
                Times.Once);


        }
    }
}
