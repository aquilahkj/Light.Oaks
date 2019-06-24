using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Light.Oaks
{
    /// <summary>
    /// Excepton type model.
    /// </summary>
    public class ExceptonTypeModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionType"></param>
        /// <param name="exceptionFunc"></param>
        public ExceptonTypeModel(Type exceptionType, Func<Exception, ExceptionModel> exceptionFunc)
        {
            ExceptionType = exceptionType;
            ExceptionFunc = exceptionFunc;
        }

        /// <summary>
        /// The exception type.
        /// </summary>
        public Type ExceptionType { get; }

        /// <summary>
        /// The exception func.
        /// </summary>
        public Func<Exception, ExceptionModel> ExceptionFunc { get; }
    }
}
