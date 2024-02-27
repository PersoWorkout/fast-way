using Application.Services.Authorization;

namespace Application.UnitTests.Services
{
    public class TokenServiceTest
    {
        [Fact]
        public void Generate_ShouldBeGenerateAUniqueToken()
        {
            //Arrange
            //Act
            var firstToken = TokenService.Generate();
            var secondToken = TokenService.Generate();

            

            Assert.NotEqual(firstToken, secondToken);
        }
    }
}
