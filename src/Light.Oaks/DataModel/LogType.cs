using System;
namespace Light.Oaks
{
    /// <summary>
    /// Log Type
    /// </summary>
    [Flags]
    public enum LogType
    {
        /// <summary>
        /// no log 
        /// </summary>
        NoLog = 0,
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
        LogTraceId = 8,
        /// <summary>
        /// Log all data
        /// </summary>
        LogAll = LogMessage | LogPostData | LogFullException | LogTraceId
    }
}
