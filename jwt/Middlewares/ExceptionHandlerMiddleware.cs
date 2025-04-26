using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace jwt.Middlewares
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro não tratado.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string errorMessage = "Ocorreu um erro interno no servidor.";

            switch (exception)
            {
                case ApplicationException appEx:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = appEx.Message;
                    break;
                // Adicione outros tipos de exceção específicas que você quer tratar de forma diferente
                // case UnauthorizedAccessException unauthorizedEx:
                //     statusCode = HttpStatusCode.Unauthorized;
                //     errorMessage = "Você não tem permissão para acessar este recurso.";
                //     break;
            }

            httpContext.Response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                StatusCode = (int)statusCode,
                Message = errorMessage
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await httpContext.Response.WriteAsync(jsonResponse);
        }
    }
}