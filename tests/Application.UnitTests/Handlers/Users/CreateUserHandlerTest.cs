using Application.Commands.Users;
using Application.Handlers.Users;
using Application.Interfaces;
using Application.Services.Authorization;
using Application.UnitTests.Configuration.Mappers;
using AutoMapper;
using Domain.DTOs.Users.Response;
using Domain.Errors;
using Domain.Models;
using Domain.ValueObjects;
using Moq;

namespace Application.UnitTests.Handlers.Users
{
    public class CreateUserHandlerTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly IMapper _mapper;
        private readonly HashService _hashService;
        private readonly CreateUserHandler _handler;

        public CreateUserHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            _mapper = MapperConfigurator.CreateMapperForUserProfile();
            _hashService = new HashService();

            _handler = new(_mockedUserRepository.Object, _mapper, _hashService);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenEmailAlreadyUsed()
        {
            var command = new CreateUserCommand()
            {
                Firstname = "john",
                Lastname = "Doe",
                Email = EmailValueObject.Create("john.doe@example.com").Data!,
                Password = PasswordValueObject.Create("Password123!").Data!
            };

            _mockedUserRepository.Setup(
                x => x.EmailAlreadyUsed(It.IsAny<EmailValueObject>()))
                .ReturnsAsync(true);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(EmailErrors.Invalid, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenAllIsValid()
        {
            //Arrange
            var command = new CreateUserCommand()
            {
                Firstname = "john",
                Lastname = "Doe",
                Email = EmailValueObject.Create("john.doe@example.com").Data!,
                Password = PasswordValueObject.Create("Password123!").Data!
            };

            _mockedUserRepository.Setup(
                x => x.EmailAlreadyUsed(It.IsAny<EmailValueObject>()))
                .ReturnsAsync(false);

            _mockedUserRepository.Setup(
                x => x.Create(It.IsAny<User>()))
                .ReturnsAsync(new User());

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<UserForList>(result.Data);
        }

    }
}
