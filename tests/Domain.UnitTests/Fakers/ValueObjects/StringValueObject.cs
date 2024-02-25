using Domain.Abstractions;

namespace Domain.UnitProject.Fakers.ValueObjects
{
    internal class StringValueObject : ValueObject
    {
        public string Value { get; set; }

        public StringValueObject(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
