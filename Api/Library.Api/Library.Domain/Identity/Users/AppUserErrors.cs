namespace Library.Domain.Identity.Users;
public static class AppUserErrors
{
    public static Error IdRequired=>Error.Validation("AppUserErrors.IdRequired", "Must provide a valid id");
    public static Error TokenRequired => Error.Validation("AppUserErrors.TokenRequired", "Must provide a valid token");
    public static Error EmailRequired => Error.Validation("AppUserErrors.EmailRequired", "Must provide a valid Email");
    public static Error UserNameRequired => Error.Validation("AppUserErrors.UserNameRequired", "Must provide a valid User Name ");
    public static Error PhoneRequired => Error.Validation("AppUserErrors.PhoneRequired", "Must provide a valid Phone");
}
