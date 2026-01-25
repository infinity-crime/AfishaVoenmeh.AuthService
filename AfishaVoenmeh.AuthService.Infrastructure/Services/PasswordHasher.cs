using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace AfishaVoenmeh.AuthService.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int _iterations = 100000;
    private const int _saltSize = 16;
    private const int _hashSize = 32;

    private readonly ILogger<PasswordHasher> _logger;

    public PasswordHasher(ILogger<PasswordHasher> logger)
    {
        _logger = logger;
    }

    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            _iterations,
            _hashSize));

        string result = $"{_iterations}.{Convert.ToBase64String(salt)}.{hashed}";

        return result;
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var parts = hashedPassword.Split('.', 3);
        if (parts.Length != 3) 
            return false;

        if (!int.TryParse(parts[0], out int iterations))
            return false;

        byte[] salt;
        byte[] storedHash;

        try
        {
            salt = Convert.FromBase64String(parts[1]);
            storedHash = Convert.FromBase64String(parts[2]);
        }
        catch(FormatException ex)
        {
            _logger.LogError($"Password/Salt convert failed: {ex.Message}");

            return false;
        }

        byte[] computedHash = KeyDerivation.Pbkdf2(
            password: providedPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: iterations,
            numBytesRequested: storedHash.Length);

        return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
    }
}