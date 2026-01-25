using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

namespace AfishaVoenmeh.AuthService.Application.Common.DTOs;

public record RefreshSessionResult(
    UserId UserId, 
    string Token, 
    DateTime ExpiresAt);
