using AfishaVoenmeh.AuthService.Domain.Common.Abstract;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    protected PhoneNumber() { } // EF Core

    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber Create(string phoneNumber) => new(phoneNumber);


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}