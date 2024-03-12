using Application.Services.Authorization;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Presentation.FunctionalTests.Authentication
{
    public class LogoutControllerTest: BaseFunctionalTest
    {
        private readonly AuthDbContext _authDbContext;

        public LogoutControllerTest(FunctionalWebApplicationFactory factory): 
            base(factory) 
        {
            _authDbContext = _scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        }

        private const string Endpoint = "auth/logout";

        [Fact]
        public async Task Handle_ShouldReturnUnauthorized_WhenUserNotLogged()
        {
            //Arrange
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, Endpoint);

            //Act
            var result = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task Handle_ShouldReturnNoContent()
        {
            //Arrange
            var token = TokenService.Generate();
            var userId = Guid.NewGuid();

            var session = new Session(userId, _hashService.Hash(token)!);

            await _authDbContext.AddAsync(session);
            await _authDbContext.SaveChangesAsync();

            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, Endpoint);
            httpRequest.Headers.Add("Authorization", token);

            //Act
            var result = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }
    }
}
