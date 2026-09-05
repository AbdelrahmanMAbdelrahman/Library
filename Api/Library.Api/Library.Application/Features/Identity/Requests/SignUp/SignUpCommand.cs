namespace Library.Application.Features.Identity.Requests.SignUp;

public sealed record SignUpCommand(string Name,string Email,string UserName,string Phone,string Password):
    IRequest<Result<Success>>;
