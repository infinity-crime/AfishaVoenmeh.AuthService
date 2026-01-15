using AfishaVoenmeh.AuthService.Domain.Common.Abstract;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

public class PasswordHash : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    protected PasswordHash() { } // Ef Core

    private PasswordHash(string value) => Value = value;

    public static ErrorOr<PasswordHash> Create(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            return Error.Validation("PasswordHash_Null", "Password hash cannot be null.");

        return new PasswordHash(hash);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}