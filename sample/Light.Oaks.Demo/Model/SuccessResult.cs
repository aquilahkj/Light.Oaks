using System;
namespace Light.Oaks.Demo.Model
{
    public class SuccessResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Light.WebApi.Core.SuccessResult"/> class.
        /// </summary>
        public SuccessResult() : this(1, "success")
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Light.WebApi.Core.SuccessResult"/> class.
        /// </summary>
        /// <param name="code">Code.</param>
        public SuccessResult(int code) : this(code, "success")
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Light.WebApi.Core.SuccessResult"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public SuccessResult(string message) : this(1, message)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Light.WebApi.Core.SuccessResult"/> class.
        /// </summary>
        /// <param name="code">Code.</param>
        /// <param name="message">Message.</param>
        public SuccessResult(int code, string message)
        {
            Code = code;
            Message = message;
        }
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
    }
}
