using Microsoft.AspNetCore.Diagnostics;

namespace Library.Api.Infrastructure
{
    public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            ProblemDetailsContext problemDetailsContext = new ProblemDetailsContext {
            HttpContext = httpContext,
            Exception=exception,
            ProblemDetails=new ProblemDetails()
            {
                Type=exception.GetType().Name,
                Detail=exception.Message,
                Title= "Application Errors"
            }
            };
          return await problemDetailsService.TryWriteAsync(problemDetailsContext);
            
        }
    }
}
