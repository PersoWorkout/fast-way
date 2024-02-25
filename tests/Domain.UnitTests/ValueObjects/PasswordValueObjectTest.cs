using Domain.ValueObjects;

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

        [Fact]
        public void Equals_ShouldReturnTrue_WhenTwoValuesAreIdentic()
        {
            //Arrange
            const string PasswordValue = "Password123!";

            var firstValue = PasswordValueObject.Create(PasswordValue).Data;
            var secondValue = PasswordValueObject.Create(PasswordValue).Data;

            //Act
            var result = firstValue.Equals(secondValue);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenTwoValuesAreDifferent()
        {
            //Arrange
            const string PasswordValue = "Password123!";
            const string secondPasswordValue = "OtherPassword123!";

            var firstValue = PasswordValueObject.Create(PasswordValue).Data;
            var secondValue = PasswordValueObject.Create(secondPasswordValue).Data;

            //Act
            var result = firstValue.Equals(secondValue);

            //Assert
            Assert.False(result);
        }
    }
}
