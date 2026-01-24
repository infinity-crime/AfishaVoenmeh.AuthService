using AfishaVoenmeh.AuthService.Domain.Common.Abstract;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

public class PasswordHash : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    protected PasswordHash() { } // Ef Core

    private PasswordHash(string value) => Value = value;

    public static PasswordHash Create(string hash) => new(hash);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}