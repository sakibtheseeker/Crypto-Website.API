using Crypto_Website.API.Helper;
using Crypto_Website.Application.DTO;
using Crypto_Website.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Crypto_Website.API.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        ILogger<ExceptionHandler> _logger;
        public ExceptionHandler(ILogger<ExceptionHandler> logger) 
        {
            this._logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            //var response = new ErrorMessage
            //{
            //    statusCode = httpContext.Response.StatusCode,
            //    Title = "something went wrong",
            //    Message = exception.Message
            //};
            int statusCode;
            string message;

            switch (exception)
            {
                case UnauthorizedException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    message = exception.Message;
                    break;

                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = exception.Message;
                    break;

                case BadRequestException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = exception.Message;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "Something went wrong. Please try again later.";

                    _logger.LogError(exception, "Unhandled exception occurred");
                    break;
            }

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            var response = ApiResponse<string>.FailureResponse(
                code: statusCode.ToString(),
                Details: message
            );

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
            
        }
    }
}
