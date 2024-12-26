using CommonShared.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Common.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(httpContext, ex, ex.StatusCode, ex.Message);
            }
            catch (ApplicationException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, HttpStatusCode statusCode, string message)
        {
            string errorMessage = exception.InnerException?.Message ?? exception.Message;

            _logger.LogError(exception, errorMessage);

            var response = httpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            var errorResponse = new ResponseApi<object>(
                response: new
                {
                    StatusCode = (int)statusCode,
                    ErrorMessage = errorMessage,
                },
                result: false
            );

            var json = JsonConvert.SerializeObject(errorResponse, Formatting.Indented);
            await response.WriteAsync(json);
        }
    }
}
