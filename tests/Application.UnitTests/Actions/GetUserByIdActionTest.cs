using Application.Actions.Users;
using Application.Queries;
using Domain.Errors;
using MediatR;
using Moq;

namespace Application.UnitTests.Actions
{
    public class GetUserByIdActionTest
    {
        private readonly Mock<ISender> _mockedSender;
        private readonly GetUserByIdAction _action;

        public GetUserByIdActionTest()
        {
            _mockedSender = new Mock<ISender>();
            _action = new GetUserByIdAction(_mockedSender.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailureResult_WhenIdIsInvalidGuid()
        {
            //Arrange
            const string Id = "Invalid Guid";

            //Act
            var result = await _action.Execute(Id);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(UserErrors.NotFound(Id), result.Errors);
            _mockedSender.Verify(
                m => m.Send(It.IsAny<GetUserByIdQuery>(), default),
                Times.Never());
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccessResult_WhenidIsValidGuid()
        {
            //Arrange
            string Id = Guid.NewGuid().ToString();

            //Act
            var result = await _action.Execute(Id);

            //Assert
            _mockedSender.Verify(
                m => m.Send(It.IsAny<GetUserByIdQuery>(), default),
                Times.Once());
        }
    }
}
