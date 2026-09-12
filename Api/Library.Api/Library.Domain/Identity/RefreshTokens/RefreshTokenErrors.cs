namespace Library.Domain.Identity.RefreshTokens;

internal static class RefreshTokenErrors
{
    internal static Error IdRequired => Error.Validation("RefreshTokenErrors.IdReuired","Provide a valid id");
    internal static Error TokenRequired => Error.Validation("RefreshTokenErrors.TokenRequired","Provide a valid token");
    internal static Error UserIdRequired => Error.Validation("RefreshTokenErrors.UserIdRequired","Provide a valid user id");
    internal static Error InvalidExpirationDate => Error.Validation("RefreshTokenErrors.InvalidExpiration","Provide a valid expiration date");
}
