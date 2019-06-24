using System;
namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExceptionResultModule
    {
        /// <summary>
        /// Creates output result data
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        object CreateResult(Exception exception, ErrorResult result);
    }
}
