using Application.Services.Authorization;
using Domain.Models;
using Domain.ValueObjects;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Presentation.FunctionalTests.Authentication
{
    public class LoginControllerController: 
        BaseFunctionalTest
    {
        private readonly ApplicationDbContext _dbContext;

        public LoginControllerController(FunctionalWebApplicationFactory factory): 
            base(factory)
        {
            _dbContext = _scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();
        }

        private const string Endpoint = "auth/login";

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenPayloadIsInvalid()
        {
            //Arrange
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoint);

            var payload = new Dictionary<string, dynamic>
            {
                { "email", "" },
                { "password", "" }
            };

            httpRequest.Content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "text/json");

            //Act
            var result = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkAndToken()
        {
            //Arrange
            const string EmailValue = "login.test@example.com";
            const string PasswordValue = "Password123!";

            var hashedPassword = _hashService.Hash(PasswordValue);

            var user = new User()
            {
                Firstname = "Login",
                Lastname = "Test",
                Email = EmailValueObject.Create(EmailValue).Data!,
                Password = PasswordValueObject.Create(hashedPassword!).Data!
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoint);

            var payload = new Dictionary<string, dynamic>
            {
                { "email", EmailValue },
                { "password", PasswordValue }
            };

            httpRequest.Content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "text/json");

            //Act
            var result = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

    }
}
