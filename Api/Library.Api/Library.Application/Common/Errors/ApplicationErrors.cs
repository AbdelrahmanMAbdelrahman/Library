







namespace Library.Application.Common.Errors;

public sealed class ApplicationErrors
{
    internal static Result<Updated> FineAlreadyPaid;

    internal static Result<BorrowingRecordDto> RecordAlreadyBorrowed=>Error.Conflict("ApplicationErrors.RecordAlreadyBorrowed");

    internal static Error FineNotFound(Guid Id)=>Error.NotFound($"No Fine found for id = {Id}");
    internal static Error BookNotFound(Guid Id)=>Error.NotFound($"No book found for id = {Id}");

    public static Error FileNotFound(Guid Id)=> Error.NotFound($"No file found for id = {Id} ");

    internal static Error CopyNotFound(Guid Id) => Error.NotFound($"No copy found with id = {Id}");
    internal static Error BorrowingRecordNotFound(Guid id) => Error.NotFound($"No Borrowing Record found with id = {id}");

    internal static Error NameAlreadyToken(string Name)=>Error.BadRequest($"{Name} already used");

    internal static Error InvalidExpireAccessToken(string AccessToken)=>
        Error.BadRequest("ApplicationErrors.InvalidExpireAccessToken",$"{AccessToken} is not valid");
    internal static Error UserNotFound(string Id)=> Error.NotFound(
        "ApplicationErrors.UserNotFound",$"User with Id ={Id} is not found");
    internal static Error RefreshTokenNotFound(string RefreshToken)=>
        Error.BadRequest("ApplicationErrors.RefreshTokenNotFound",$"{RefreshToken} is not found");

 
}
