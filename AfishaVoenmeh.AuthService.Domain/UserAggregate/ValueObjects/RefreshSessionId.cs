using AfishaVoenmeh.AuthService.Domain.Common.Abstract;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

public class RefreshSessionId : ValueObject
{
    public Guid Value { get; protected set; }

    private RefreshSessionId(Guid value) => Value = value;

    public static RefreshSessionId CreateUnique() => new(Guid.NewGuid());

    public static RefreshSessionId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
