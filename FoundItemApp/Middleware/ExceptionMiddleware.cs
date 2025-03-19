using System.Net;
using System.Text.Json;

namespace FoundItemApp.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            } catch (Exception ex)
            {
                _logger.LogError("Error {ex} has occured during at {method}, {path}. {message}", ex, context.Request.Method, context.Request.Path, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = new
                {
                    StatusCode = context.Response.StatusCode.ToString(), 
                    Message = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
