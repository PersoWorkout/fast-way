using Application.Actions.Users;
using Application.Queries;
using MediatR;
using Moq;

namespace Application.UnitTests.Actions
{
    public class GetUsersActionTest
    {
        private readonly Mock<IMediator> _mockedMediator;
        private readonly GetUsersAction _action;

        public GetUsersActionTest()
        {
            _mockedMediator = new Mock<IMediator>();
            _action = new GetUsersAction(_mockedMediator.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnSucessResult()
        {
            //Arrange
            //Act
            var result = await _action.Execute();

            //Assert
            _mockedMediator.Verify(
                m => m.Send(It.IsAny<GetUsersQuery>(), default),
                Times.Once());
        }
    }
}
