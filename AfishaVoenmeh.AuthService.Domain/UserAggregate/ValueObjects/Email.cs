using AfishaVoenmeh.AuthService.Domain.Common.Abstract;
using AfishaVoenmeh.AuthService.Domain.Common.Errors;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    protected Email() { } // EF Core

    private Email(string value) => Value = value;

    public static ErrorOr<Email> Create(string email)
    {
        if (!IsValidEmail(email))
            return DomainErrors.InvalidEmail;

        return new Email(email);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        email = email.Trim();

        try
        {
            var address = new MailAddress(email);

            if(address.Address == email)
            {
                return address.Host.EndsWith("voenmeh.ru", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
        catch(Exception)
        {
            return false;
        }
    }
}