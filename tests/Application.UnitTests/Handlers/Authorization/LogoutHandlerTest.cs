using Application.Commands.Authorization;
using Application.Handlers.Authorization;
using Application.Interfaces;
using Application.Services.Authorization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Handlers.Authorization
{
    public class LogoutHandlerTest
    {
        private readonly Mock<IAuthorizationRepository> _mockedAuthRepository;
        private readonly LogoutHandler _handler;

        public LogoutHandlerTest()
        {
            _mockedAuthRepository = new Mock<IAuthorizationRepository>();
            _handler = new(_mockedAuthRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult()
        {
            //Arrange
            var command = new LogoutCommand 
            { 
                Token = TokenService.Generate() 
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);

            _mockedAuthRepository.Verify(
                x => x.DestroyByToken(It.IsAny<string>()),
                Times.Once());
        }

    }
}
