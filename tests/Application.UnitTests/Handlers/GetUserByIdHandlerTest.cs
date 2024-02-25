using Application.Handlers.Users;
using Application.Interfaces;
using Application.Queries;
using Domain.Errors;
using Domain.Models;
using Moq;

namespace Application.UnitTests.Handlers
{
    public class GetUserByIdHandlerTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;

        public GetUserByIdHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
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

            var handler = new GetUserByIdHandler(_mockedUserRepository.Object);

            //Act
            var result = await handler.Handle(query, default);

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

            var handler = new GetUserByIdHandler(_mockedUserRepository.Object);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<User>(result.Data);
        }
    }
}
