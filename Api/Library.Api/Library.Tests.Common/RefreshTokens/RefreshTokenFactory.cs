using Library.Domain.Common.Results;
using Library.Domain.Identity.RefreshTokens;

namespace Library.Tests.Common.RefreshTokens;

public class RefreshTokenFactory
{
    public static Result<RefreshToken> Create(Guid? id, string? token, string? userId, DateTime? expireOn)
    {
        return RefreshToken.Create(
            id??Guid.NewGuid(),
            token??"oijdfjklsf",
            userId??Guid.NewGuid().ToString(),
            expireOn??DateTimeOffset.UtcNow.LocalDateTime

            );
    }
}
