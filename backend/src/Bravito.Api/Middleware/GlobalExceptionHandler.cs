using Microsoft.AspNetCore.Diagnostics;

namespace Bravito.Api.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = "Ocorreu um erro interno no servidor.",
                    // Não retorna stack trace em produção ou mesmo em desenvolvimento no formato público por segurança
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
