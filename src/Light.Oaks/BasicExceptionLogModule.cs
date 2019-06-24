using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;

namespace Light.Oaks
{
    class BasicExceptionLogModule : IExceptionLogModule
    {
        private readonly ILogger logger;

        public BasicExceptionLogModule(ILogger<BasicExceptionLogModule> logger)
        {
            this.logger = logger;
        }

        public void LogError(HttpContext httpContext, Exception exception, ExceptionModel model, ErrorResult result)
        {
            bool logMessage = (model.LogType & LogType.LogMessage) == LogType.LogMessage;

            bool logPostData = (model.LogType & LogType.LogPostData) == LogType.LogPostData;

            bool logFullException = (model.LogType & LogType.LogFullException) == LogType.LogFullException;

            bool logTraceId = (model.LogType & LogType.LogTraceId) == LogType.LogTraceId;

            bool log = logMessage || logPostData || logFullException || logTraceId;

            if (logger != null && log) {
                var user = httpContext.GetAccount();
                var action = httpContext.Request.Method + " " + httpContext.Request.Path.Value;
                if (httpContext.Request.QueryString.HasValue) {
                    action += " " + httpContext.Request.QueryString.Value;
                }
                var token = httpContext.GetToken();
                var sb = new StringBuilder();
                sb.Append($"type:\"{exception.GetType().FullName}\"");
                sb.Append($",message:\"{FormatString(exception.Message)}\"");
                sb.Append($",action:\"{FormatString(action)}\"");
                if (token != null)
                    sb.Append($",token:\"{FormatString(token)}\"");
                if (user != null)
                    sb.Append($",user:\"{FormatString(user)}\"");
                sb.Append($",http_code:\"{model.HttpStatus}\"");
                if (logTraceId) {
                    sb.Append($",traceid:\"{result.TraceId}\"");
                }
                sb.Append($",err_code:\"{result.Code}\",err_msg:\"{FormatString(result.Message)}\"");

                if (logPostData) {
                    if (httpContext.Request.ContentLength.HasValue && httpContext.Request.ContentLength.Value > 0) {
                        httpContext.Request.EnableRewind();
                        httpContext.Request.Body.Seek(0, 0);
                        string content;
                        using (StreamReader sr = new StreamReader(httpContext.Request.Body, Encoding.UTF8)) {
                            content = sr.ReadToEnd();
                        }
                        if (!string.IsNullOrEmpty(content)) {
                            sb.AppendLine();
                            sb.AppendLine("***Post Data Start***");
                            sb.AppendLine(content);
                            sb.AppendLine("***Post Data End***");
                        }
                    }
                }
                if (!logFullException) {
                    logger.LogError(sb.ToString());
                }
                else {
                    logger.LogError(exception, sb.ToString());
                }
            }
        }

        string FormatString(string data)
        {
            if (data.IndexOf('\n', StringComparison.CurrentCulture) >= 0) {
                data = data.Replace('\n', ' ');
            }
            if (data.IndexOf('"', StringComparison.CurrentCulture) >= 0) {
                data = data.Replace("\"", "\\\"");
            }
            return data;
        }
    }
}
