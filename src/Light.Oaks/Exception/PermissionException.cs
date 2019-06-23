using System;

namespace Light.Oaks
{
    /// <summary>
    /// Permission exception.
    /// </summary>
    public class PermissionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Light.Oaks.PermissionException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public PermissionException(string message) : base(message)
        {

        }


        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <value>The account.</value>
        public string Account { get; }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>The action.</value>
        public string Action { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Light.Oaks.PermissionException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="account">Account.</param>
        /// <param name="action">Action.</param>
        public PermissionException(string message, string account, string action) : base(message)
        {
            this.Account = account;
            this.Action = action;
        }
    }
}