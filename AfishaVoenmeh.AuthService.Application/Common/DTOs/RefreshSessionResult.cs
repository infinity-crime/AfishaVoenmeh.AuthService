namespace AfishaVoenmeh.AuthService.Application.Common.DTOs;

public record RefreshSessionResult(string Token, DateTime ExpiresAt);
