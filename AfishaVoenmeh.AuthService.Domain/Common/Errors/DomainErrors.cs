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

    public static Error EmptyUserId =>
        Error.Validation("UserId_Empty", "User Id cannot be empty.");
}