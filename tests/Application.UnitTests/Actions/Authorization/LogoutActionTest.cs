using Application.Actions.Authorization;
using Application.Commands.Authorization;
using Application.Services.Authorization;
using Domain.Abstractions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Actions.Authorization
{
    public class LogoutActionTest
    {
        private readonly Mock<ISender> _sender;
        private readonly LogoutAction _action;

        public LogoutActionTest()
        {
            _sender = new Mock<ISender>();

            _action = new(_sender.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccessResult()
        {
            //Arrange
            string token = TokenService.Generate();

            _sender.Setup(
                x => x.Send(It.IsAny<LogoutCommand>(), default))
                .ReturnsAsync(Result<object>.Success());

            //Assert
            var result = await _action.Execute(token);

            //Assert
            Assert.True(result.IsSucess);
        }
    }
}
