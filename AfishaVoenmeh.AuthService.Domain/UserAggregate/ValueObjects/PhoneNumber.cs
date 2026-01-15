using AfishaVoenmeh.AuthService.Domain.Common.Abstract;
using AfishaVoenmeh.AuthService.Domain.Common.Errors;
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
            return DomainErrors.EmptyPhoneNumber;

        if (!phoneNumber.StartsWith("+7"))
            return DomainErrors.InvalidPhoneNumberFormat;

        if (phoneNumber.Length != 12)
            return DomainErrors.InvalidPhoneNumberLength;

        return new PhoneNumber(phoneNumber);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}