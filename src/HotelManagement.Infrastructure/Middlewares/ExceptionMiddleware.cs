using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace HotelManagement.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex, _env.IsDevelopment());
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, bool includeDetail)
        {
            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new ErrorDetails
            {
                Status = 500,
                Title = includeDetail ? "An error occured: " + exception.Message : "An error occured",
                Detail = includeDetail ? exception.ToString() : null
            };

            await httpContext.Response.WriteAsync(error.ToString());
        }
    }
}

public class ErrorDetails
{
    public int Status { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
