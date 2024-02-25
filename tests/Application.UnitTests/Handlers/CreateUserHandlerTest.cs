using Application.Commands.Users;
using Application.Handlers.Users;
using Application.Interfaces;
using Application.UnitTests.Configuration.Mappers;
using AutoMapper;
using Domain.DTOs.Users.Response;
using Domain.Errors;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Handlers
{
    public class CreateUserHandlerTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly IMapper _mapper;
        private readonly CreateUserHandler _handler;

        public CreateUserHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();
            _mapper = MapperConfigurator.CreateMapperForUserProfile();

            _handler = new(_mockedUserRepository.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenEmailAndPasswordAreNotValid()
        {
            //Arrange
            var command = new CreateUserCommand()
            {
                Firstname = "john",
                Lastname = "Doe",
                Email = "john.doe",
                Password = "pass"
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(EmailErrors.Invalid, result.Errors);
            Assert.Contains(PasswordErrors.Invalid, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenEmailAlreadyUsed()
        {
            var command = new CreateUserCommand()
            {
                Firstname = "john",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!"
            };

            _mockedUserRepository.Setup(
                x => x.EmailAlreadyUsed(It.IsAny<string>()))
                .ReturnsAsync(true);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Contains(EmailErrors.Invalid, result.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenAllIsValid()
        {
            //Arrange
            var command = new CreateUserCommand()
            {
                Firstname = "john",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!"
            };

            _mockedUserRepository.Setup(
                x => x.EmailAlreadyUsed(It.IsAny<string>()))
                .ReturnsAsync(false);

            _mockedUserRepository.Setup(
                x => x.Create(It.IsAny<User>()))
                .ReturnsAsync(new User());

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<UserForList>(result.Data);
        }

    }
}
