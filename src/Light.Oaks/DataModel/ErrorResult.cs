using System;
//using Newtonsoft.Json;

namespace Light.Oaks
{
    /// <summary>
    /// Error result.
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ErrorData[] Errors { get; set; }

        /// <summary>
        /// Gets or sets the Trace Id.
        /// </summary>
        /// <value>The errors.</value>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TraceId { get; set; }
    }
}
