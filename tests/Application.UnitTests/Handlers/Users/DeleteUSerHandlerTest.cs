using Application.Commands.Users;
using Application.Handlers.Users;
using Application.Interfaces;
using Domain.Errors;
using Domain.Models;
using Moq;

namespace Application.UnitTests.Handlers.Users
{
    public class DeleteUserHandlerTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly Mock<IAuthorizationRepository> _mockedAuthRepository;
        private readonly DeleteUserHandler _handler;

        public DeleteUserHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            _mockedAuthRepository = new Mock<IAuthorizationRepository>();
            _handler = new(
                _mockedUserRepository.Object,
                _mockedAuthRepository.Object);
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

            _mockedAuthRepository.Verify(
                x => x.DestroyByUser(It.IsAny<Guid>()),
                Times.Never());
        }

        [Fact]
        public async Task Handle_ReturnResultSuccess_WhenAllIsValid()
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

            _mockedAuthRepository.Verify(
                x => x.DestroyByUser(It.IsAny<Guid>()),
                Times.Once());
        }
    }
}
