using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Contracts.Requests;

public record RegisterUserRequest(
    string FirstName, 
    string LastName, 
    string Patronymic, // Отчество
    string PhoneNumber,
    string Email,
    string Password,
    string PasswordConfirmation);