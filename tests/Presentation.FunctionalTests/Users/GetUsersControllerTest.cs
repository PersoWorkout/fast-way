using Application.Interfaces;
using Application.Services.Authorization;
using Domain.Enums;
using Domain.Models;
using Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Presentation.FunctionalTests.Users
{
    public class GetUsersControllerTest: BaseFunctionalTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authRepository;
        private readonly HashService _hashService;
        public GetUsersControllerTest(FunctionalWebApplicationFactory factory): base(factory) 
        {
            _userRepository = _scope.ServiceProvider.GetRequiredService<IUserRepository>();
            _authRepository = _scope.ServiceProvider.GetRequiredService<IAuthorizationRepository>();
            _hashService = _scope.ServiceProvider.GetRequiredService<HashService>();
        }

        private const string Endpoint = "/users";

        [Fact]
        public async Task Handle_ShouldReturnSuccess()
        {
            //Arrange
            var emailValue = EmailValueObject.Create("john.doe.admin@example.com").Data;
            var passwordValue = PasswordValueObject.Create("Password123!").Data;

            var adminUser = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = emailValue!,
                Password = passwordValue!,
                Role = UserRoles.Administrateur
            };

            var user = await _userRepository.Create(adminUser);

            var token = TokenService.Generate();
            var session = new Session(user.Id, _hashService.Hash(token)!);

            await _authRepository.CreateSession(session);

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, Endpoint);
            httpRequest.Headers.Add("Authorization", token);

            //Act
            var result = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.OK ,result.StatusCode);
        }
    }
}
