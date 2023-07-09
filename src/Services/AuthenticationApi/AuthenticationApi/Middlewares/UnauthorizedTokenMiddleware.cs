using System.Net;
using System.Text.Json;
using Shared.Entities;
using Shared.Factories;
using Shared.Logs.Services;

namespace AuthenticationApi.Middlewares
{
    public class UnauthorizedTokenMiddleware : IMiddleware
    {
        private readonly ILogServices _logServices;

        public UnauthorizedTokenMiddleware(ILogServices logServices)
        {
            _logServices = logServices;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized
                || context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                const string dataType = @"application/problem+json";
                const int statusCode = StatusCodes.Status401Unauthorized;


                var commandResult = await CommandResult.FailAsync(
                    new List<string> { 
                        "Você não possui acesso a essa operação.",
                        "Acesso negado" });

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = dataType;

                _logServices.LogData.AddResponseStatusCode(statusCode)
                                    .AddResponseBody(commandResult)
                                    .AddRequestUrl(context.Request.Path)
                                    .AddRequestQuery(context.Request.QueryString.Value);

                _logServices.WriteLog();

                await context.Response.WriteAsync(JsonSerializer.Serialize(commandResult, JsonOptionsFactory.GetSerializerOptions()));
            }
        }
    }
}