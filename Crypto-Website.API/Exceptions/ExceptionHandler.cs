using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Crypto_Website.Application.DTO;


namespace Crypto_Website.API.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        ILogger<ExceptionHandler> Logger;
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            this.Logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new ErrorMessage
            {
                statusCode = 500,
                Title = "Something went wrong",
                Message = exception.Message
            };

            Logger.LogError(exception, exception.Message);

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }

    }
}