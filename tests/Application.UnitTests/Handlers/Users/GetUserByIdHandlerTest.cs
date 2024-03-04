using Application.Handlers.Users;
using Application.Interfaces;
using Application.Queries;
using Domain.Errors;
using Domain.Models;
using Moq;

namespace Application.UnitTests.Handlers.Users
{
    public class GetUserByIdHandlerTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;

        private readonly GetUserByIdHandler _handler;

        public GetUserByIdHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();

            _handler = new(_mockedUserRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenUserNotFOund()
        {
            //Arrange
            var id = Guid.NewGuid();
            var query = new GetUserByIdQuery(id);

            _mockedUserRepository.Setup(
                x => x.GetById(id))
                .ReturnsAsync(() => null);

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(
                UserErrors.NotFound(id.ToString()),
                result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSucessResult_WhenUserExist()
        {
            //Arrange
            var id = Guid.NewGuid();
            var query = new GetUserByIdQuery(id);

            _mockedUserRepository.Setup(
                x => x.GetById(id))
                .ReturnsAsync(new User());

            //Act
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<User>(result.Data);
        }
    }
}
