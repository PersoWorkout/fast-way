using Application.Commands.Users;
using Application.Handlers.Users;
using Application.Interfaces;
using Application.UnitTests.Configuration.Mappers;
using AutoMapper;
using Domain.DTOs.Users.Response;
using Domain.Errors;
using Domain.Models;
using Domain.ValueObjects;
using Moq;

namespace Application.UnitTests.Handlers.Users
{
    public class UpdateUserHandlerTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly IMapper _mapper;
        private readonly UpdateUserHandler _handler;

        public UpdateUserHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            _mapper = MapperConfigurator.CreateMapperForUserProfile();

            _handler = new(_mockedUserRepository.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
        {
            //Arrange
            var id = Guid.NewGuid();

            var command = new UpdateUserCommand()
            {
                Id = id,
                Firstname = "John",
                Lastname = "Doe",
                Email = EmailValueObject.Create("john.doe@example.com").Data
            };

            _mockedUserRepository.Setup(
                x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _handler.Handle(command, default);

            //Asert
            Assert.True(result.IsFailure);
            Assert.Contains(
                UserErrors.NotFound(id.ToString()),
                result.Errors);

            _mockedUserRepository.Verify(
                x => x.Update(It.IsAny<User>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailValueIsAlreadyUsed()
        {
            //Arrange
            var id = Guid.NewGuid();

            var command = new UpdateUserCommand()
            {
                Id = id,
                Firstname = "John",
                Lastname = "Doe",
                Email = EmailValueObject.Create("john.doe@example.com").Data
            };

            _mockedUserRepository.Setup(
                x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            _mockedUserRepository.Setup(
                x => x.EmailAlreadyUsed(command.Email.Value))
                .ReturnsAsync(true);

            //Act
            var result = await _handler.Handle(command, default);

            //Asert
            Assert.True(result.IsFailure);
            Assert.Contains(
                EmailErrors.Invalid,
                result.Errors);

            _mockedUserRepository.Verify(
                x => x.Update(It.IsAny<User>()),
                Times.Never);
        }


        [Fact]
        public async Task Handle_ReturnSuccesResult_WhenAllIsValid()
        {
            //Arrange
            var id = Guid.NewGuid();

            var command = new UpdateUserCommand()
            {
                Id = id,
                Email = EmailValueObject.Create("john.doe@example.com").Data
            };

            _mockedUserRepository.Setup(
                x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(new User());

            _mockedUserRepository.Setup(
                x => x.EmailAlreadyUsed(command.Email.Value))
                .ReturnsAsync(false);

            _mockedUserRepository.Setup(
                x => x.Update(It.IsAny<User>()))
                .ReturnsAsync(new User());

            //Act
            var result = await _handler.Handle(command, default);

            //Asert
            Assert.True(result.IsSucess);
            Assert.IsType<UserDetails>(result.Data);

            _mockedUserRepository.Verify(
                x => x.Update(It.IsAny<User>()),
                Times.Once);
        }
    }
}
