namespace Library.Api.Controllers;

[ApiController]
public class ApiController:ControllerBase
{
    protected ActionResult Problem(List<Error> errors)
    {
        if (errors.Count == 0) throw new InvalidOperationException();
        if (errors.All(error => error.Type == ErrorKind.Validation))
        {
            return ValidationProblem(errors);
        }
        return Problem(errors[0]);
    }

    private ObjectResult Problem(Error error)
    {
        int statusCode = error.Type switch
        {
            ErrorKind.NotFound => StatusCodes.Status404NotFound,
            ErrorKind.Validation => StatusCodes.Status400BadRequest,
            ErrorKind.InternalServerError => StatusCodes.Status500InternalServerError,
            ErrorKind.UnAuthorized => StatusCodes.Status401Unauthorized,
            ErrorKind.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
        return  Problem(statusCode:statusCode,title:error.Description);
    }

    private ActionResult ValidationProblem(List<Error> errors)
    {
        ModelStateDictionary keyValuePairs = new ModelStateDictionary();
        errors.ForEach(error => keyValuePairs.TryAddModelError(error.Code, error.Description));
        return ValidationProblem(keyValuePairs);
    }
}
