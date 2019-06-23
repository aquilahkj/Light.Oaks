using System;

namespace Light.Oaks
{
    /// <summary>
    /// Authorize exception.
    /// </summary>
    public class AuthorizeException : Exception
    {
        public AuthorizeErrorType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Light.Oaks.AuthorizeException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public AuthorizeException(AuthorizeErrorType type, string message) : base(message)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Light.Oaks.AuthorizeException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public AuthorizeException(AuthorizeErrorType type, string message, Exception innerException) : base(message, innerException)
        {
            Type = type;
        }
    }

    public enum AuthorizeErrorType
    {
        /// <summary>
        /// The token is not exists
        /// </summary>
        TokenNotExists,
        /// <summary>
        /// The token is format error
        /// </summary>
        TokenError,
        /// <summary>
        /// The user does not login
        /// </summary>
        AccountNotLogin,
        /// <summary>
        /// The user has logged in elsewhere
        /// </summary>
        AccountHasLoginElsewhere
    }
}