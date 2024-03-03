using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Presentation.FunctionalTests.Authentication
{
    public class RegisterControllerTest(FunctionalWebApplicationFactory factory) : 
        BaseFunctionalTest(factory)
    {
        private const string Endpoint = "auth/register";

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenPayloadIsInvalid()
        {
            //Arrange
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoint);

            var payload = new Dictionary<string, dynamic>
            {
                { "firstname", "" },
                { "lastname", "test" },
                { "email", "test" },
                { "password", "" }
            };

            httpRequest.Content = new StringContent(
                JsonConvert.SerializeObject(payload), 
                Encoding.UTF8, 
                "text/json");

            //Act
            var response = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenAllIsValid()
        {
            //Arrange
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, Endpoint);

            var payload = new Dictionary<string, dynamic>
            {
                { "firstname", "firstTest" },
                { "lastname", "firstTest" },
                { "email", "first.test@example.com" },
                { "password", "Password123!" },
                { "passwordConfirmation", "Password123!" }
            };

            httpRequest.Content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "text/json");

            //Act
            var response = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
