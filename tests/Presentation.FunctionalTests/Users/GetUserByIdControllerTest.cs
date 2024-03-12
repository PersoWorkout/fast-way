using System.Net;

namespace Presentation.FunctionalTests.Users
{
    public class GetUserByIdControllerTest : BaseFunctionalTest
    {
        public GetUserByIdControllerTest(FunctionalWebApplicationFactory factory) : 
            base(factory)
        {
        }

        protected const string Endpoint = "/users";

        [Fact]
        public async Task Handle_ShouldReturnUnothorized_WhenUserIsNotAdmin()
        {
            //Arrange
            var userId = Guid.NewGuid();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{Endpoint}/{userId}");

            //Act
            var response = await _client.SendAsync(httpRequest);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
