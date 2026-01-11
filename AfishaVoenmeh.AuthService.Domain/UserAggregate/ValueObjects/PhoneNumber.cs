using AfishaVoenmeh.AuthService.Domain.Common.Abstract;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    protected PhoneNumber() { } // EF Core

    private PhoneNumber(string value) => Value = value;

    public static ErrorOr<PhoneNumber> Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Error.Validation("PhoneNumber_Null", "Phone number cannot be null.");

        if (!phoneNumber.StartsWith("+7"))
            return Error.Validation("PhoneNumber_Invalid", "Phone number starts with +7");

        if (phoneNumber.Length != 12)
            return Error.Validation("PhoneNumber_Length_Invalid", "Length phone number must be 11.");

        return new PhoneNumber(phoneNumber);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}