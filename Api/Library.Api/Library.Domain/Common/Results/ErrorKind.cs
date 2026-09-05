namespace Library.Domain.Common.Results;

public  enum ErrorKind
{
    NotFound,
    UnAuthorized,
    InternalServerError,
    BadRequest ,
    Conflict,
    TooManyRequests,
    Failure,
    Forbidden,
    Validation
}
