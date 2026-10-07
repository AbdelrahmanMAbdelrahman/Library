using Library.Domain.Common.Results;
using Library.Domain.Identity.Users;

namespace Library.Tests.Common.AppUsers;

public class AppUserFactory

{
    public static Result<AppUser> Create(string? name = null, string? email = null,
        string? phone = null, string? userName = null)
    {
        return AppUser.Create(name??"john",email??"john@gmail.com",
            phone??"01114308227",userName?? "john@gmail.com");
    }
}
