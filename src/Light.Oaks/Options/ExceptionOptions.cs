using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ExceptonTypeModel> ExceptionTypes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseOkStatus { get; set; }
    }
}
