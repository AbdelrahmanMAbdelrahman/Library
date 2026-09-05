namespace Library.Domain.Common.Results
{
    public readonly record struct Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorKind Type { get; }
        public Error(string code,string description,ErrorKind type)
        {
            this.Code = code;
            this.Description = description;
            this.Type = type;
        }
        public static Error NotFound(string code=nameof(NotFound),string description="Not Found Error") =>
            new Error(code,description,ErrorKind.NotFound);
        public static Error UnAuthorized(string code=nameof(UnAuthorized),string description="Un Authorized Error") =>
            new Error(code,description,ErrorKind.UnAuthorized);
        public static Error InternalServerError(string code=nameof(InternalServerError),string description= "Internal Server Error") =>
            new Error(code,description,ErrorKind.NotFound);
        public static Error BadRequest(string code=nameof(BadRequest),string description= "Bad Request Error") =>
            new Error(code,description,ErrorKind.NotFound);
        public static Error Conflict(string code=nameof(Conflict),string description= "Conflict Error") =>
            new Error(code,description,ErrorKind.NotFound);
        public static Error TooManyRequests(string code=nameof(TooManyRequests),string description= "Too Many Requests Error") =>
            new Error(code,description,ErrorKind.NotFound);
        public static Error Failure(string code=nameof(Failure),string description= "Failure") =>
            new Error(code,description,ErrorKind.Failure);
        public static Error Forbidden(string code=nameof(Forbidden),string description= "Forbidden") =>
            new Error(code,description,ErrorKind.Forbidden);
        public static Error Validation(string code=nameof(Validation),string description= "Validation") =>
            new Error(code,description,ErrorKind.Validation);
    }
}
