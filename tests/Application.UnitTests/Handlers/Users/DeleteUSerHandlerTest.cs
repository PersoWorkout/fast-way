using Application.Commands.Users;
using Application.Handlers.Users;
using Application.Interfaces;
using Domain.Errors;
using Domain.Models;
using Moq;

namespace Application.UnitTests.Handlers.Users
{
    public class DeleteUSerHandlerTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly DeleteUserHandler _handler;

        public DeleteUSerHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            _handler = new(_mockedUserRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnFailureResult_WhenUserNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();

            var command = new DeleteUserCommand(userId);

            _mockedUserRepository.Setup(
                x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(
                UserErrors.NotFound(userId.ToString()),
                result.Errors);
        }

        [Fact]
        public async Task Handle_ReturnResultFailure_WhenAllIsValid()
        {
            var userId = Guid.NewGuid();

            var command = new DeleteUserCommand(userId);

            _mockedUserRepository.Setup(
                x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
