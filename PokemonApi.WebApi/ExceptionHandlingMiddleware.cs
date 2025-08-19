using PokemonApi.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace PokemonApi.WebApi
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "NotFoundException");
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "ExternalServiceException");
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadGateway);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ungandled Exception");
                await HandleExceptionAsync(context, "ocorreu um erro inesperado.", HttpStatusCode.InternalServerError);
            }


        }

        private Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new { error = message });
            return context.Response.WriteAsync(result);
        }
    }
}
