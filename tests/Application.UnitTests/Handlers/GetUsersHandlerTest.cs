using Application.Handlers.Users;
using Application.Interfaces;
using Application.Queries;
using Domain.Models;
using Moq;

namespace Application.UnitTests.Handlers
{
    public class GetUsersHandlerTest
    {
        private Mock<IUserRepository> _mockedUserRepository;

        public GetUsersHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult()
        {
            //Arrange
            var query = new GetUsersQuery();

            _mockedUserRepository.Setup(
                x => x.GetAll())
                .ReturnsAsync([]);

            var handler = new GetUsersHandler(_mockedUserRepository.Object);

            //Act

            var result = await handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<List<User>>(result.Data);
        }
    }
}
