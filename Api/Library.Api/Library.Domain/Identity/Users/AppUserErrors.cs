namespace Library.Domain.Identity.Users;
internal static class AppUserErrors
{
    internal static Error IdRequired=>Error.Validation("AppUserErrors.IdRequired", "Must provide a valid id");
    internal static Error TokenRequired => Error.Validation("AppUserErrors.TokenRequired", "Must provide a valid token");
    internal static Error EmailRequired => Error.Validation("AppUserErrors.EmailRequired", "Must provide a valid Email");
    internal static Error UserNameRequired => Error.Validation("AppUserErrors.UserNameRequired", "Must provide a valid User Name ");
    internal static Error PhoneRequired => Error.Validation("AppUserErrors.PhoneRequired", "Must provide a valid Phone");
}
