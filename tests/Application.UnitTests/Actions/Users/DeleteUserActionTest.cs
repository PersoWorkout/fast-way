using Application.Actions.Users;
using Application.Commands.Users;
using Domain.Errors;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Actions.Users
{
    public class DeleteUserActionTest
    {
        private readonly Mock<ISender> _mockedSender;
        private readonly DeleteUserAction _action;

        public DeleteUserActionTest()
        {
            _mockedSender = new Mock<ISender>();
            _action = new DeleteUserAction(_mockedSender.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFailureResult_WhenIdIsNotAValidGuid()
        {
            //Arrange
            const string InvalidId = "Invalid Guid";

            //Act
            var result = await _action.Execute(InvalidId);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(
                UserErrors.NotFound(InvalidId),
                result.Errors);

            _mockedSender.Verify(
                x => x.Send(It.IsAny<DeleteUserCommand>(), default),
                Times.Never);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccessResult_WhenAllIsValid()
        {
            //Arrange
            string UserId = Guid.NewGuid().ToString();

            //Act
            var result = await _action.Execute(UserId);

            //Assert
            _mockedSender.Verify(
                x => x.Send(It.IsAny<DeleteUserCommand>(), default),
                Times.Once);
        }
    }
}
