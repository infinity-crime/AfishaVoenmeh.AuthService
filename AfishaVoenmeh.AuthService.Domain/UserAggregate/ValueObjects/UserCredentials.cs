using AfishaVoenmeh.AuthService.Domain.Common.Abstract;

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

    public static UserCredentials Create(string firstName, string lastName, string patronymic) 
        => new(firstName, lastName, patronymic);


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return Patronymic;
    }
}