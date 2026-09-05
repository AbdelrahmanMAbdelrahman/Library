
namespace Library.Application.Features.Identity.Requests.SignUp;
public class SignUpHandler : IRequestHandler<SignUpCommand, Result<Success>>
{
    public Task<Result<Success>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
