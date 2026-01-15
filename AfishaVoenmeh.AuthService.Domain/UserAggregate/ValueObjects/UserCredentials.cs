using AfishaVoenmeh.AuthService.Domain.Common.Abstract;
using AfishaVoenmeh.AuthService.Domain.Common.Errors;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

public class UserCredentials : ValueObject
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Patronymic { get; private set; } = string.Empty;

    protected UserCredentials() { } // EF Core

    private UserCredentials(string firstName, string lastName, string patronymic)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    public static ErrorOr<UserCredentials> Create(string firstName, string lastName, string patronymic)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return DomainErrors.EmptyCredsField;

        if(string.IsNullOrWhiteSpace(lastName))
            return DomainErrors.EmptyCredsField;

        if (string.IsNullOrWhiteSpace(patronymic))
            return DomainErrors.EmptyCredsField;

        return new UserCredentials(firstName, lastName, patronymic);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return Patronymic;
    }
}