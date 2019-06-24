using System;
namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExceptionProcessModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        ExceptionModel ProcessException(Exception ex);
    }
}
