using AfishaVoenmeh.AuthService.Domain.Common.Abstract;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; private set; }

    protected UserId() { } // EF Core

    private UserId(Guid value) => Value = value;

    public static ErrorOr<UserId> CreateFrom(Guid id)
    {
        if (id == Guid.Empty)
            return Error.Validation("UserId_Empty", "User Id cannot be empty.");

        return new UserId(id);
    }

    public static UserId CreateUnique() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}