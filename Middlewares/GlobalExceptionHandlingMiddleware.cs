using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApplication1.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {

        private readonly ILogger _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {

            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var problemDetails = new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = ex.InnerException.ToString(),
                    Type = ex.GetType().ToString() // Set the actual type of the exception
                };

                // Return the ProblemDetails as JSON
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                LogExceptionDetails(ex);
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        private void LogExceptionDetails(Exception ex)
        {
            // Log exception details including stack trace and inner exceptions
            _logger.LogError(ex, $"Exception Details: {ex.Message}\nStackTrace: {ex.StackTrace}");

            if (ex.InnerException != null)
            {
                LogExceptionDetails(ex.InnerException);
            }
        }
    }
}
