using Library.Domain.Common.Results;
using Library.Domain.Identity.Users;

namespace Library.Domain.Tests.AppUsers;

public class AppUserTests
{
   [Fact]
   public void Create_ShouldPass_ForValidData( )
    {
        string name = "nada";
        string email = "nada@gmail.com";
        string phone = "01114308227";
        string userName = "nada@gmail.com";

       Result< AppUser> userRes = AppUser.Create(name,email,phone,userName);
        Assert.True(userRes.IsSuccess);
        AppUser user = userRes.Value;
        Assert.Equal(user.Name,name);
        Assert.Equal(user.Email,email);
        Assert.Equal(user.PhoneNumber,phone);
        Assert.Equal(user.UserName,userName);
    }
    [Theory]
    [InlineData("", "nada@gmail.com", "01114308227", "nada@gmail.com")]
    [InlineData("nada", "", "01114308227", "nada@gmail.com")]
    [InlineData("nada", "nada@gmail.com", "", "nada@gmail.com")]
    [InlineData("nada", "nada@gmail.com", "01114308227", "")]
    public void Create_ShouldFail_ForInValidData(string name, string email, string phone, string userName)
    {
        Result<AppUser> userRes = AppUser.Create(name, email, phone, userName);
        Assert.False(userRes.IsSuccess);
    }
    }
