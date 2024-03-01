using Application.Actions.Users;
using Application.Commands.Users;
using Application.UnitTests.Configuration.Mappers;
using Application.Validators.Users;
using Domain.DTOs.Users.Request;
using Domain.Enums;
using MediatR;
using Moq;

namespace Application.UnitTests.Actions.Users
{
    public class CreateUserActionTest
    {

        private readonly Mock<ISender> _mockedSender;
        private readonly CreateUserAction _action;

        public CreateUserActionTest()
        {
            _mockedSender = new Mock<ISender>();
            _action = new(
                _mockedSender.Object,
                MapperConfigurator.CreateMapperForUserProfile(),
                new CreateUserRequestValidator());
        }

        [Fact]
        public async Task Execute_ShouldReturnFailureResult_WhenPayloadIsNotValid()
        {
            //Arrange
            var request = new CreateUserRequest()
            {
                Firstname = string.Empty,
                Lastname = string.Empty,
                Email = string.Empty,
                Password = string.Empty,
                Role = 0
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsFailure);
            Assert.NotEmpty(result.Errors);

            _mockedSender.Verify(
                m => m.Send(It.IsAny<CreateUserCommand>(), default),
                Times.Never());
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccess_WhenPayloadIsValid()
        {
            //Arrange
            var request = new CreateUserRequest()
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!",
                Role = (int)UserRoles.Client,
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            _mockedSender.Verify(
                m => m.Send(It.IsAny<CreateUserCommand>(), default),
                Times.Once());
        }
    }
}
