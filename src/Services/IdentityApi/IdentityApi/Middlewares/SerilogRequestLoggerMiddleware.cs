﻿using System.Text;
using Shared.Logs.Services;

namespace IdentityApi.Middlewares
{
    public class SerilogRequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogServices _logService;

        public SerilogRequestLoggerMiddleware(RequestDelegate next,
                                              ILogServices logService)
        {
            _logService = logService;
            _next = next;
        }

        /// <summary>
        /// Efetua a leitura do HttpContext para recuperar as informações de request e response para os logs
        /// da aplicação.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            string requestBody = await GetRequestBodyAsync(context);

            var originalRequestBodyReference = context.Response.Body;

            ConsolidarInformacaoDeLogs(requestBody, context);

            using var requestBodyMemoryStream = new MemoryStream();

            await _next(context);

            await requestBodyMemoryStream.CopyToAsync(originalRequestBodyReference);
        }

        private static async Task<string> GetRequestBodyAsync(HttpContext httpContext)
        {
            var requestBody = string.Empty;

            if (httpContext.Request.ContentLength is null)
                return requestBody;

            HttpRequestRewindExtensions.EnableBuffering(httpContext.Request);
            Stream body = httpContext.Request.Body;
            byte[] buffer = new byte[Convert.ToInt32(httpContext.Request.ContentLength)];

            await httpContext.Request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length));

            requestBody = Encoding.UTF8.GetString(buffer);

            body.Seek(0, SeekOrigin.Begin);

            httpContext.Request.Body = body;

            return requestBody;
        }

        private void ConsolidarInformacaoDeLogs(string requestBody, HttpContext httpContext)
        {
            if (requestBody is null)
            {
                return;
            }

            if (httpContext.Request.QueryString.HasValue)
            {
                _logService.LogData.AddRequestQuery(httpContext.Request.QueryString.Value);
            }

            _logService.LogData.AddRequestBody(requestBody)
                               .AddRequestType(httpContext.Request.Method)
                               .AddRequestUrl(httpContext.Request.Path)
                               .AddTraceIdentifier(httpContext.TraceIdentifier);
        }

    }
}