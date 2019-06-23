using System;
namespace Light.Oaks
{
    [Flags]
    public enum LogType
    {
        /// <summary>
        /// no log 
        /// </summary>
        None = 0,
        /// <summary>
        /// Only log the exception message
        /// </summary>
        LogMessage = 1,
        /// <summary>
        /// Log the http request body data 
        /// </summary>
        LogPostData = 2,
        /// <summary>
        /// Log full message
        /// </summary>
        LogFullException = 4,
        /// <summary>
        /// Create TraceId and log
        /// </summary>
        LogTraceId = 8
    }
}
