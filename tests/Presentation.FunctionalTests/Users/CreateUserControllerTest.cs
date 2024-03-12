using Application.Interfaces;
using Application.Services.Authorization;
using Domain.Enums;
using Domain.Models;
using Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Presentation.FunctionalTests.Users
{
    public class CreateUserControllerTest : BaseFunctionalTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authRepository;

        public CreateUserControllerTest(FunctionalWebApplicationFactory factory) : base(factory)
        {
            _userRepository = _scope.ServiceProvider.GetRequiredService<IUserRepository>();
            _authRepository = _scope.ServiceProvider.GetRequiredService<IAuthorizationRepository>();
        }

        private const string Endpoint = "/users";

        [Fact]
        public async Task Handle_ShouldReturnUnothorized_WhenUserIsNotAdmin()
        {
            //Arrange
            var payload = new Dictionary<string, string>();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoint);

            httpRequest.Content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "text/json");

            //Act
            var response = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenPayloadIsNotValid()
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

            var payload = new Dictionary<string, string>();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoint);

            httpRequest.Content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "text/json");

            httpRequest.Headers.Add("Authorization", token);

            //Act
            var result = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk()
        {
            //Arrange
            var emailValue = EmailValueObject.Create("john.doe.admin2@example.com").Data;
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

            var payload = new Dictionary<string, string>()
            {
                {"firstname", "John" },
                {"lastname", "Doe" },
                {"email", "john.doe@example.com" },
                {"password", "Password123!" },
                {"role", "2" }
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoint);

            httpRequest.Content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "text/json");

            httpRequest.Headers.Add("Authorization", token);

            //Act
            var result = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
