using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Light.Oaks
{
    /// <summary>
    /// Excepton type model.
    /// </summary>
    public class ExceptonTypeModel
    {
        public ExceptonTypeModel(Type exceptionType, Func<Exception, ExceptionModel> exceptionFunc)
        {
            ExceptionType = exceptionType;
            ExceptionFunc = exceptionFunc;
        }

        public Type ExceptionType { get; }

        /// <summary>
        /// The exception func.
        /// </summary>
        public Func<Exception, ExceptionModel> ExceptionFunc { get; }
    }
}
