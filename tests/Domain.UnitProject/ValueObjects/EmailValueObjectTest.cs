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
}
