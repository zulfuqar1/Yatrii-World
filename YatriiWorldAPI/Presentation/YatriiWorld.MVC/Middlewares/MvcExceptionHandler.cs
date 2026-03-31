using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using YatriiWorld.MVC.Exceptions;

namespace YatriiWorld.MVC.Middlewares
{
    public class MvcExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<MvcExceptionHandler> _logger;

        public MvcExceptionHandler(ILogger<MvcExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "App error: {Message}", exception.Message);

            if (exception is AlreadyExistsException)
                return false;

            var (code, message) = exception switch
            {
                NotFoundException ex => (404, ex.Message),
                BadRequestException ex => (400, ex.Message),
                ForbiddenException ex => (403, ex.Message),
                UnauthorizedAccessException => (401, "You do not have permission to perform this action."),
                _ => (500, "An unexpected error occurred in the system. Please try again later.")
            };

          
            var errorPath = $"/Home/Error?message={Uri.EscapeDataString(message)}&code={code}";

            httpContext.Response.Redirect(errorPath);

            return true;
        }
    }
}