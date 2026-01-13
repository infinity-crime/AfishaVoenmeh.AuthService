using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Application.Common.Errors;

public static class AuthenticationErrors
{
    public static Error DublicateEmail => 
        Error.Conflict("User_Email_Exists", $"A user with this email already exists.");

    public static Error PasswordConfirmationFailed =>
        Error.Conflict("Password_Confirmation_Failed", "Passwords must match.");

}