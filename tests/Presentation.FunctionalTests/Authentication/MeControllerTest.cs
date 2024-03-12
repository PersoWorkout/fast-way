using Application.Services.Authorization;
using Domain.Models;
using Domain.ValueObjects;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Presentation.FunctionalTests.Authentication
{
    public class MeControllerTest : BaseFunctionalTest
    {
        private readonly AuthDbContext _authDbContext;
        private readonly ApplicationDbContext _applicationDbContext;

        public MeControllerTest(FunctionalWebApplicationFactory factory) : base(factory)
        {
            _authDbContext = _scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            _applicationDbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        private const string Endpoint = "/auth/me";

        [Fact]
        public async Task Handle_ShouldReturnUnauthorized_WhenNotLogged()
        {
            //Arrange
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, Endpoint);

            //Act
            var result = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenUserNotExist()
        {
            //Arrange
            var token = TokenService.Generate();

            var session = new Session(Guid.NewGuid(), _hashService.Hash(token)!);

            await _authDbContext.Sessions.AddAsync(session);
            await _authDbContext.SaveChangesAsync();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, Endpoint);
            httpRequest.Headers.Add("Authorization", token);

            //Act
            var result = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenUserIsLogged()
        {
            //Arrange
            var emailValue = EmailValueObject.Create("user.me@example.com");
            var passwordValue = PasswordValueObject.Create(_hashService.Hash("Password123!")!);

            var user = new User
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = emailValue.Data!,
                Password = passwordValue.Data!
            };

            await _applicationDbContext.Users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();

            var token = TokenService.Generate();

            var session = new Session(user.Id, _hashService.Hash(token)!);

            await _authDbContext.Sessions.AddAsync(session);
            await _authDbContext.SaveChangesAsync();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, Endpoint);
            httpRequest.Headers.Add("Authorization", token);

            //Act
            var result = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
