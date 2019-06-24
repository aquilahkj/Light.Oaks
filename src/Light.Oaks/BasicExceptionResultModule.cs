using System;
namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicExceptionResultModule : IExceptionResultModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public object CreateResult(Exception exception, ErrorResult result)
        {
            return result;
        }
    }


}
