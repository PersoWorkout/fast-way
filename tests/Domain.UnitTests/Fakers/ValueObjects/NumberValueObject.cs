using Domain.Abstractions;

namespace Domain.UnitProject.Fakers.ValueObjects
{
    public class NumberValueObject : ValueObject
    {
        public int Value { get; set; }

        public NumberValueObject(int value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
