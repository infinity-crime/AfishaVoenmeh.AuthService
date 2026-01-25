namespace AfishaVoenmeh.AuthService.Application.Authentication.Common;

public record RefreshTokenResult(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiration);
