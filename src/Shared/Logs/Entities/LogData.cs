using System.Text.Json;
using Shared.Helpers;

namespace Shared.Logs.Entities
{
    public class LogData
    {
        public DateTime Timestamp { get; private set; }
        public object RequestData { get; private set; }
        public string? RequestMethod { get; private set; }
        public string? RequestUri { get; private set; }
        public string? RequestQuery { get; private set; }
        public object ResponseData { get; private set; }
        public int ResponseStatusCode { get; private set; }
        public string? TraceId { get; private set; }
        public Exception Exception { get; private set; }
        public string? Mensagem { get; private set; } = "Requisição efetuada";

        public LogData()
        {
            Timestamp = DateTime.Now;
        }

        public LogData AddMessageInformation(string? mensagem)
        {
            Mensagem = mensagem;
            return this;
        }

        public LogData AddResponseStatusCode(int codigo)
        {
            ResponseStatusCode = codigo;
            return this;
        }

        public LogData AddRequestType(string metodo)
        {
            RequestMethod = metodo;
            return this;
        }

        public LogData AddRequestUrl(string? url)
        {
            RequestUri = url;
            return this;
        }

        public LogData AddTraceIdentifier(string? indentificador)
        {
            TraceId = indentificador;
            return this;
        }

        public LogData AddRequestBody(object requestData)
        {
            string json = JsonSerializer.Serialize(requestData);
            if (requestData is null)
                RequestData = "No Request Data";
            else if (json.Length > 250)
                RequestData = "Request Data is too large to log";
            else
                RequestData = requestData;

            return this;
        }

        public LogData AddRequestBody(string requestData)
        {
            if (string.IsNullOrEmpty((string)requestData))
                RequestData = "No Request Data";
            else if (requestData.Length > 250)
                RequestData = "Request Data is too large to log";
            else
                RequestData = requestData;

            return this;
        }

        public LogData AddRequestQuery(string requestQuery)
        {
            if (string.IsNullOrEmpty((string)requestQuery))
                RequestData = "No Request Query";
            else
                RequestQuery = requestQuery;

            return this;
        }

        public LogData AddResponseBody(object responseData)
        {
            var data = JsonSerializer.Serialize(responseData);
            if (data.Length > 250)
            {
                ResponseData = $"Response data is too large to log";

            }
            else
            {
                ResponseData = responseData;

            }

            return this;
        }

        public LogData AddException(Exception exception)
        {
            Exception = exception;
            return this;
        }

        public LogData ClearLogData()
        {
            Timestamp = DateTimeExtensions.GetGmtDateTime(DateTime.Now);
            RequestData = string.Empty;
            RequestMethod = string.Empty;
            RequestUri = string.Empty;
            ResponseData = string.Empty;
            ResponseStatusCode = 0;
            TraceId = string.Empty;

            return this;
        }

        public LogData ClearLogExceptionData()
        {
            Exception = default;

            return this;
        }
    }
}