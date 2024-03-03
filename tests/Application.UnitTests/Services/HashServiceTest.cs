using Application.Services.Authorization;

namespace Application.UnitTests.Services
{
    public class HashServiceTest
    {
        private readonly HashService _hashService = new();

        [Fact]
        public void Hash_ShouldReturnHashedString()
        {
            //Arrange
            const string Password = "Password";

            //Act
            var hashedPassword = _hashService.Hash(Password);

            //Assert
            Assert.NotEmpty(hashedPassword);
            Assert.NotEqual(Password, hashedPassword);
        }

        [Fact]
        public void Hash_ShouldReturnNull_WhenValueIsNull()
        {
            //Arrange
            //Act
            var hashedPassword = _hashService.Hash(null);

            //Assert
            Assert.Null(hashedPassword);
        }

        [Fact]
        public void Verify_ShouldReturnFalse_WhenTwoValueAreDifferent()
        {
            //Arrange
            const string FirstValue = "FirstValue";
            const string SecondValue = "SecondValue";

            string hashedValue = BCrypt.Net.BCrypt.HashPassword(FirstValue);

            //Arrange
            var result = _hashService.Verify(SecondValue, hashedValue);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Verify_ShouldReturnTrue_WhenTwoBasedValueAreIdentic()
        {
            //Arrange
            const string value = "PasswordTest";
            string hashedValue = BCrypt.Net.BCrypt.HashPassword(value);

            //Arrange
            var result = _hashService.Verify(value, hashedValue);

            //Assert
            Assert.True(result);
        }
    }
}
