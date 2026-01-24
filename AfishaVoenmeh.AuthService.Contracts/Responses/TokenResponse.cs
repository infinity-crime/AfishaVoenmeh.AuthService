namespace AfishaVoenmeh.AuthService.Contracts.Responses;

public record TokenResponse(
    string AccessToken, 
    string RefreshToken, 
    DateTime RefreshTokenExpiration);
