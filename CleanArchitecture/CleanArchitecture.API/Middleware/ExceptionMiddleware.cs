using CleanArchitecture.API.Errors;
using System.Net;
using System.Text.Json;

namespace CleanArchitecture.API.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context )
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _hostEnvironment.IsDevelopment()
                    ? new CodeErrorException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) // Is Development
                    : new CodeErrorException((int)HttpStatusCode.InternalServerError); // Is Produccion 

                var options = new JsonSerializerOptions { PropertyNamingPolicy= JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
