using Application.Services.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Services
{
    public class HashServiceTest
    {
        [Fact]
        public void Hash_ShouldReturnHashedString()
        {
            //Arrange
            const string Password = "Password";

            //Act
            var hashedPassword = HashService.Hash(Password);

            //Assert
            Assert.NotEmpty(hashedPassword);
            Assert.NotEqual(Password, hashedPassword);
        }

        [Fact]
        public void Hash_ShouldReturnNull_WhenValueIsNull()
        {
            //Arrange
            //Act
            var hashedPassword = HashService.Hash(null);

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
            var result = HashService.Verify(SecondValue, hashedValue);

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
            var result = HashService.Verify(value, hashedValue);

            //Assert
            Assert.True(result);
        }
    }
}
