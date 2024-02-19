using Domain.UnitProject.Fakers.ValueObjects;

namespace Domain.UnitProject.Abstractions
{
    public class ValueObjectTest
    {
        [Fact]
        public void EqualOperator_ShouldReturnTrue_WhenTwoValueObjectAreEqual()
        {
            //Arrange
            const int ObjectValue = 5;
            var firstValueObject = new NumberValueObject(ObjectValue);
            var secondValueObject = new NumberValueObject(ObjectValue);

            //Act
            var result = NumberValueObject.Equals(firstValueObject, secondValueObject);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualOperator_ShouldReturnFalse_WhenTwoValueObjectsAreDifferent()
        {
            //Arrange
            const int ObjectValue = 5;
            const int OtherObjectValue = 10;
            var firstValueObject = new NumberValueObject(ObjectValue);
            var secondValueObject = new NumberValueObject(OtherObjectValue);

            //Act
            var result = Equals(firstValueObject, secondValueObject);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualOperator_ShouldReturnFalse_WhenTwoValueObjectsAreDifferentType()
        {
            //Arrange
            const int ObjectValue = 5;
            const string OtherObjectValue = "5";
            var firstValueObject = new NumberValueObject(ObjectValue);
            var secondValueObject = new StringValueObject(OtherObjectValue);

            //Act
            var result = Equals(firstValueObject, secondValueObject);

            //Assert
            Assert.False(result);
        }
    }
}
