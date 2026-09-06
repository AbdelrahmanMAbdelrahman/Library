
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Features.Identity.Requests.SignUp;
public class SignUpHandler(IIdentityService identityService,ILogger<SignUpHandler>logger) : IRequestHandler<SignUpCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
       Result<Success> signUpResult =await identityService
            .SignUp(request.Name,request.Email,request.Phone,request.UserName,request.Password);
        if (signUpResult.IsError)
        {
            logger.LogError("Error Sign up");
            return signUpResult.Errors;
        }

        //send email confirmation later
        return signUpResult.Value;
    }
}
