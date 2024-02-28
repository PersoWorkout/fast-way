using Application.Actions.Authorization;
using Application.Commands.Authorization;
using Application.UnitTests.Configuration.Mappers;
using Application.Validators.Authorization;
using Domain.Abstractions;
using Domain.DTOs.Authorization;
using Domain.DTOs.Authorization.Requests;
using MediatR;
using Moq;

namespace Application.UnitTests.Actions.Authorization
{
    public class LoginActionTest
    {
        private readonly Mock<ISender> _mockedSender;
        private readonly LoginAction _action;

        public LoginActionTest()
        {
            _mockedSender = new Mock<ISender>();

            _action = new(_mockedSender.Object,
                MapperConfigurator.CreateMapperForAuthProfile(),
                new LoginRequestValidation());
        }

        [Fact]
        public async Task Execute_ShouldReturnFailureResult_WhenRequestIsNotValid()
        {
            //Arrange
            var request = new LoginRequest()
            {
                Email = "john.doe",
                Password = "Password"
            };

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsFailure);
            
            _mockedSender.Verify(
                x => x.Send(It.IsAny<LoginCommand>(), default),
                Times.Never);
        }

        [Fact]
        public async Task Execute_ShouldReturnSuccessResult_WhenAllIsValid()
        {
            //Arrange
            var request = new LoginRequest()
            {
                Email = "john.doe@example.com",
                Password = "Password123!"
            };

            _mockedSender.Setup(
                x => x.Send(It.IsAny<LoginCommand>(), default))
                .ReturnsAsync(Result<ConnectedResponse>.Success());

            //Act
            var result = await _action.Execute(request);

            //Assert
            Assert.True(result.IsSucess);

            _mockedSender.Verify(
                x => x.Send(It.IsAny<LoginCommand>(), default),
                Times.Once);
        }
    }
}
