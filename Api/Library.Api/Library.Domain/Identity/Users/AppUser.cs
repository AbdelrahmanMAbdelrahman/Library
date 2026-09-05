namespace Library.Domain.Identity.Users;

public sealed class AppUser:IdentityUser
{
    public string? Name { get;private set; }
    private AppUser(){}
    private AppUser(string id,string name,string email,string phone,string userName){

        Id = id;
        Name = name;   
        Email = email;
        PhoneNumber = phone;
        UserName = userName;
            }
    public static Result<AppUser> Create( string name, string email, string phone, string userName)
    {
        
        if (string.IsNullOrEmpty(name)) return AppUserErrors.TokenRequired;
        if (string.IsNullOrEmpty(email)) return AppUserErrors.EmailRequired;
        if (string.IsNullOrEmpty(phone)) return AppUserErrors.PhoneRequired;
        if (string.IsNullOrEmpty(userName)) return AppUserErrors.UserNameRequired;
        return new AppUser(Guid.NewGuid().ToString(),name,email,phone,userName);
    }

}
