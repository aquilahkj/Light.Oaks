using System;
namespace Light.Oaks
{
    /// <summary>
    /// Multi error.
    /// </summary>
    public interface IMultiError
    {
        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        ErrorData[] Errors { get; }
    }
}
