using AfishaVoenmeh.AuthService.Domain.Common.Abstract;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate;

public class User : AggregateRoot<UserId>
{
    public UserCredentials Credentials { get; private set; }
    public Email Email {  get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public PasswordHash PasswordHash { get; private set; }

    public int RoleId { get; private set; }
    public Role Role { get; private set; }

    protected User() { } // EF Core

    public User(UserCredentials credentials, Email email, PhoneNumber phoneNumber, PasswordHash passwordHash)
    {
        Id = UserId.CreateUnique();
        Credentials = credentials;
        Email = email;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
    }

    public void ApplyRole(Role role)
    {
        RoleId = role.Id;
    }
}