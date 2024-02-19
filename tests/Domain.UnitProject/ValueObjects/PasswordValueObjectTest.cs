using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitProject.ValueObjects
{
    public class PasswordValueObjectTest
    {
        [Fact] 
        public void Create_ShouldReturnSucessResult_WhenValueIsCorrectPassword()
        {
            //Arrange
            const string PasswordValue = "Password123!";

            //Act
            var valueObject = PasswordValueObject.Create(PasswordValue);

            //Assert
            Assert.True(valueObject.IsSucess);
            Assert.Equal(PasswordValue, valueObject.Data.Value);
        }

        [Fact]
        public void Create_ShouldReturnFailureResult_WhenValueIsIncorrectPassword()
        {
            //Arrange
            const string PasswordValue = "test!";

            //Act
            var valueObject = PasswordValueObject.Create(PasswordValue);

            //Assert
            Assert.True(valueObject.IsFailure);
            Assert.NotEmpty(valueObject.Errors);
        }
    }
}
