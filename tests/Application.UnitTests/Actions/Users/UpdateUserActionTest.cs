using Application.Actions.Users;
using Application.Commands.Users;
using Application.UnitTests.Configuration.Mappers;
using Application.Validators.Users;
using Domain.Abstractions;
using Domain.DTOs.Users.Request;
using Domain.DTOs.Users.Response;
using MediatR;
using Moq;

namespace Application.UnitTests.Actions.Users
{
    public class UpdateUserActionTest
    {
        private readonly Mock<ISender> _mockedSender;
        private readonly UpdateUserAction _action;

        public UpdateUserActionTest()
        {
            _mockedSender = new Mock<ISender>();

            _action = new(_mockedSender.Object,
                MapperConfigurator.CreateMapperForUserProfile(),
                new UpdateUserRequestValidator());
        }

        [Fact]
        public async Task Execute_ShouldReturnFailureResult_WhenPayloadIsNotValid()
        {
            //Arrange
            var request = new UpdateUserRequest()
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe",
                Password = "password",
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsFailure);
            _mockedSender.Verify(
                x => x.Send(It.IsAny<UpdateUserCommand>(), default),
                Times.Never);
        }

        [Fact]
        public async Task Execute_SHouldReturnSuccessResult_WhenPayloadIsValid()
        {
            var request = new UpdateUserRequest()
            {
                Email = "john.doe@example.com",
                Password = "Password123!",
            };

            _mockedSender.Setup(
                x => x.Send(It.IsAny<UpdateUserCommand>(), default))
                .ReturnsAsync(Result<UserDetails>.Success());

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsSucess);
            _mockedSender.Verify(
                x => x.Send(It.IsAny<UpdateUserCommand>(), default),
                Times.Once);
        }
    }
}
