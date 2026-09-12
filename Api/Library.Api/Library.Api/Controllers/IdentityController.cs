namespace Library.Api.Controllers;

[Route("api/[Controller]")]
public class IdentityController(ISender sender):ApiController
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status409Conflict)]
    [EndpointName(nameof(Register))]
    [EndpointSummary("Register new user")]
    [EndpointDescription("Take name ,email ,phone ,username , password then create user account in the database")]
    [HttpPost(Name ="Register")]
    public async Task<IActionResult> Register(SignUpCommand command,CancellationToken ct)
    {
        Result<Success> result = await sender.Send(command,ct);
        return result.Match(_=>Ok("Created"), Problem);
    }
    [ProducesResponseType(typeof(TokenResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [EndpointName(nameof(Login))]
    [EndpointSummary("Authenticate user")]
    [EndpointDescription("Take email, password then create access token , " +
        "refresh token and expiration date of the access token")]
    [HttpGet(Name = nameof(Login))]
    public async Task<IActionResult> Login(SignInQuery query,CancellationToken ct)
    {
        Result<TokenResponse> tokenResult = await sender.Send(query,ct);
        return tokenResult.Match(response => Ok(response), Problem);
    }
}
