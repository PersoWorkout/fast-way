using Application.Handlers.Users;
using Application.Interfaces;
using Application.Mappers;
using Application.Queries;
using AutoMapper;
using Domain.DTOs.Users.Response;
using Domain.Models;
using Moq;

namespace Application.UnitTests.Handlers
{
    public class GetUsersHandlerTest
    {
        private readonly Mock<IUserRepository> _mockedUserRepository;
        private readonly IMapper _mapper;

        public GetUsersHandlerTest()
        {
            _mockedUserRepository = new Mock<IUserRepository>();

            var mapConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<UserProfile>();
            });

            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult()
        {
            //Arrange
            var query = new GetUsersQuery();

            _mockedUserRepository.Setup(
                x => x.GetAll())
                .ReturnsAsync([]);

            var handler = new GetUsersHandler(_mockedUserRepository.Object, _mapper);

            //Act

            var result = await handler.Handle(query, default);

            //Assert
            Assert.True(result.IsSucess);
            Assert.IsType<List<UserForList>>(result.Data);
        }
    }
}
