using System;
namespace Light.Oaks
{
    public class ExceptionModel
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
        public ErrorData[] Errors { get; set; }

        /// <summary>
        /// Gets or sets the Http Status
        /// </summary>
        public int HttpStatus { get; set; }

        /// <summary>
        /// Gets or sets the Log Type
        /// </summary>
        public LogType LogType { get; set; }
    }
}
