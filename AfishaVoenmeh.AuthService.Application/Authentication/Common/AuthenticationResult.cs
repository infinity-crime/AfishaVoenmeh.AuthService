using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Application.Authentication.Common;

public record AuthenticationResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Patronymic,
    string Token);