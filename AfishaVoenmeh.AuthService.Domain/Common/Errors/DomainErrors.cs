using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Domain.Common.Errors;

public static class DomainErrors
{
    public static Error InvalidEmail =>
        Error.Validation("Email_Invalid", "Invalid email form. Host requires voenmeh.ru");

    public static Error EmptyPasswordHash =>
        Error.Validation("PasswordHash_Null", "Password hash cannot be null.");

    public static Error EmptyPhoneNumber =>
        Error.Validation("PhoneNumber_Null", "Phone number cannot be null.");

    public static Error InvalidPhoneNumberFormat =>
        Error.Validation("PhoneNumber_Invalid", "Phone number starts with +7");

    public static Error InvalidPhoneNumberLength =>
        Error.Validation("PhoneNumber_Length_Invalid", "Length phone number must be 11.");

    public static Error EmptyCredsField =>
        Error.Validation("UserCredentials_Field_Null", "Filed cannot be null.");

    public static Error EmptyUserId =>
        Error.Validation("UserId_Empty", "User Id cannot be empty.");
}