

using Microsoft.AspNetCore.Diagnostics;
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
            _logger.LogError(exception, "Exception: {Message}", exception.Message);

            if (exception is AlreadyExistsException)
                return false;

            var (code, message) = exception switch
            {
                NotFoundException ex => (404, ex.Message),
                BadRequestException ex => (400, ex.Message),
                ForbiddenException ex => (403, ex.Message),
                _ => (500, "An unexpected error occurred.")
            };

            httpContext.Response.Redirect(
                $"/Home/Error?message={Uri.EscapeDataString(message)}&code={code}");

            return true;
        }
    }
}
