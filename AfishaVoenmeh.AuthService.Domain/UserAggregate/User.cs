using AfishaVoenmeh.AuthService.Domain.Common.Abstract;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate;

public class User : AggregateRoot<UserId>
{
    public UserCredentials Credentials { get; private set; }
    public Email Email {  get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public PasswordHash PasswordHash { get; private set; }

    protected User() { } // EF Core

    public User(UserCredentials credentials, Email email, PhoneNumber phoneNumber, PasswordHash passwordHash)
    {
        Id = UserId.CreateUnique();
        Credentials = credentials;
        Email = email;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
    }
}