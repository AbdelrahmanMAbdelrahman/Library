

using Library.Domain.Common.Results;
using Library.Domain.Identity.RefreshTokens;
using Library.Infrastructure.Migrations;
using Library.Tests.Common;
using Newtonsoft.Json.Linq;

namespace Library.Domain.Tests.RefreshTokens;

public class RefreshTokenTests
{
    [Fact]
    public void Create_ShouldPass_ForValidData()
    {   FakeTimeProvider timeProvider = new FakeTimeProvider();
        Guid id= Guid.NewGuid();
        string token = "ksdfjosj";
        string userId = Guid.NewGuid().ToString();
        DateTime expireOn = timeProvider.GetUtcNow().LocalDateTime;
        Result<RefreshToken> refreshTokenRes = RefreshToken.Create(id,token,userId,expireOn.AddDays(7));
        Assert.True(refreshTokenRes.IsSuccess);
        RefreshToken rt = refreshTokenRes.Value;
        Assert.Equal(rt.Id,id);
        Assert.Equal(rt.Token,token);
        Assert.Equal(rt.UserId,userId);
        Assert.Equal(rt.ExpireOn,expireOn.AddDays(7));
    }
    [Theory]
    [InlineData("ljsdklf","")]
    public void Create_ShouldFail_ForInValidUserId(string token,  string userId)
    {
        FakeTimeProvider timeProvider = new FakeTimeProvider();
        DateTime expireOn = timeProvider.GetUtcNow().LocalDateTime;
        Result<RefreshToken> refreshTokenRes = RefreshToken.Create(Guid.NewGuid(), token, userId, expireOn.AddDays(7));
        Assert.False(refreshTokenRes.IsSuccess);
    }
    [Fact]
    public void Create_ShouldFail_ForInValidId()
    {
        string token = "ksdfjosj";
        string userId = Guid.NewGuid().ToString();
        FakeTimeProvider timeProvider = new FakeTimeProvider();
        DateTime expireOn = timeProvider.GetUtcNow().LocalDateTime;
        Result<RefreshToken> refreshTokenRes = RefreshToken.Create(Guid.Empty, token, userId, expireOn.AddDays(7));
        Assert.False(refreshTokenRes.IsSuccess);
    }
[Fact]
    public void Create_ShouldFail_ForInValidToken()
    {
        string token = "ksdfjosj";
        string userId = Guid.NewGuid().ToString();
        FakeTimeProvider timeProvider = new FakeTimeProvider();
        DateTime expireOn = timeProvider.GetUtcNow().LocalDateTime;
        Result<RefreshToken> refreshTokenRes = RefreshToken.Create(Guid.NewGuid(), token, userId, expireOn.AddDays(-7));
        Assert.False(refreshTokenRes.IsSuccess);
    }
}
