using Domain.Abstractions;

namespace Domain.UnitProject.Abstractions;

public class ResultTest
{
    private readonly Error TestError = new("Test.Error", "This error occured to test the Result class");

    [Fact]
    public void Success_ShouldBeSuccess()
    {
        //Arrange
        //Act
        var result = Result<object>.Success();

        //Assert
        Assert.True(result.IsSucess);
    }

    [Fact]
    public void Success_ShouldBeReturnRequestedData_WhenIsSuccess()
    {
        //Arrange
        var data = "data";
        //Act
        var result = Result<string>.Success(data);

        //Assert
        Assert.True(result.IsSucess && result.Data == data);
    }

    [Fact]
    public void Failure_ShouldBeFailure()
    {
        //Arrange
        //Act
        var result = Result<object>.Failure(TestError);

        //Assert
        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Failure_ShouldReturnTestError_WhenIsFailure()
    {
        //Arrange
        //Act
        var result = Result<object>.Failure(TestError);

        //Assert
        Assert.Contains(TestError, result.Errors);
    }
}

