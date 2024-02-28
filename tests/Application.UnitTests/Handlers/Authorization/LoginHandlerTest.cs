using Application.Commands.Authorization;
using Application.Handlers.Authorization;
using Application.Interfaces;
using Application.Services.Authorization;
using Application.UnitTests.Configuration.Mappers;
using AutoMapper;
using Domain.DTOs.Authorization;
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
    public class LoginHandlerTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly Mock<IAuthorizationRepository> _mockedAuthorizationRepository;
        private readonly LoginHandler _handler;

        public LoginHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            _mockedAuthorizationRepository = new Mock<IAuthorizationRepository>();

            _handler = new(
                _mockedUserRepository.Object,
                _mockedAuthorizationRepository.Object,
                MapperConfigurator.CreateMapperForAuthProfile());
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenEmailIsNotAssigned()
        {
            //Arrange
            var command = new LoginCommand()
            {
                Email = EmailValueObject.Create("john.doe@example.com").Data!,
                Password = PasswordValueObject.Create("Password123!").Data!
            };

            _mockedUserRepository.Setup(
                x => x.GetByEmail(It.IsAny<EmailValueObject>()))
                .ReturnsAsync(() => null);

            //Assert
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(AuthErrors.InvalidCreadentials, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPasswordsDontMatch()
        {
            //Arrange
            var command = new LoginCommand()
            {
                Email = EmailValueObject.Create("john.doe@example.com").Data!,
                Password = PasswordValueObject.Create("Password321?").Data!
            };

            _mockedUserRepository.Setup(
                x => x.GetByEmail(It.IsAny<EmailValueObject>()))
                .ReturnsAsync(CreateJohnDoe()); ;

            //Assert
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(AuthErrors.InvalidCreadentials, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPasswordsMatch()
        {
            //Arrange
            var existingUser = CreateJohnDoe();

            var command = new LoginCommand()
            {
                Email = existingUser.Email,
                Password = PasswordValueObject.Create("Password123!").Data!
            };

            _mockedUserRepository.Setup(
                x => x.GetByEmail(It.IsAny<EmailValueObject>()))
                .ReturnsAsync(existingUser);

            _mockedAuthorizationRepository.Setup(
                x => x.CreateSession(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(new Session()
                {
                    UserId = existingUser.Id,
                    Token = TokenService.Generate(),
                    ExpiredAt = DateTime.Now.AddMinutes(30)
                }); ;

            //Assert
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<ConnectedResponse>(result.Data);

            _mockedAuthorizationRepository.Verify(
                x => x.CreateSession(It.IsAny<Guid>(), It.IsAny<string>()),
                Times.Once());
        }

        private static User CreateJohnDoe()
        {
            var hashedPassword = HashService.Hash("Password123!")!;
            var password = PasswordValueObject.Create(hashedPassword).Data!;
            return new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = EmailValueObject.Create("john.doe@example.com").Data!,
                Password = password
            };
        }
    }
}
