using System.Text.Json;
using Shared.Entities;
using Shared.Factories;
using Shared.Logs.Services;

namespace AuthenticationApi.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogServices _logServices;

        public GlobalExceptionHandlerMiddleware(ILogServices logServices)
        {
            _logServices = logServices;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Responsavel por tratar as exceções geradas na aplicação
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const string dataType = @"application/problem+json";
            const int statusCode = StatusCodes.Status500InternalServerError;

            _logServices.LogData.AddException(exception);
            _logServices.LogData.AddResponseStatusCode(statusCode);
            _logServices.WriteLog();
            _logServices.WriteLogWhenRaiseExceptions();


            var commandResult = await CommandResult.FailAsync(exception.Message);  

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = dataType;

            await context.Response.WriteAsync(JsonSerializer.Serialize(commandResult,
                JsonOptionsFactory.GetSerializerOptions()));
        }
    }
}