using Domain.ValueObjects;

namespace Domain.UnitProject.ValueObjects;

public class EmailValueObjectTest
{
    [Fact]
    public void Create_ShouldReturnSuccessResult_WhenValueIsValidEmail()
    {
        //Arrange
        const string ValidEmail = "test.test@example.com";

        //Act
        var email = EmailValueObject.Create(ValidEmail);

        //Assert
        Assert.True(email.IsSucess);
        Assert.Equal(ValidEmail, email.Data.Value);
    }

    [Fact]
    public void Create_ShouldReturnFailureResult_WhenValueIsInvalidEmail()
    {
        //Arrange
        const string InvalidEmail = "test.test";

        //Act
        var email = EmailValueObject.Create(InvalidEmail);

        //Assert
        Assert.True(email.IsFailure);
        Assert.NotEmpty(email.Errors);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenTwoValuesAreIdentic()
    {
        //Arrange
        const string EmailValue = "test@test.fr";

        var firstValue = EmailValueObject.Create(EmailValue).Data;
        var secondValue = EmailValueObject.Create(EmailValue).Data;

        //Act
        var result = firstValue.Equals(secondValue);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenTwoValuesAreDifferent()
    {
        //Arrange
        const string FirstEmailValue = "first@test.fr";
        const string SecondEmailValue = "second@test.fr";

        var firstValue = EmailValueObject.Create(FirstEmailValue).Data;
        var secondValue = EmailValueObject.Create(SecondEmailValue).Data;

        //Act
        var result = firstValue.Equals(secondValue);

        //Assert
        Assert.False(result);
    }
}
