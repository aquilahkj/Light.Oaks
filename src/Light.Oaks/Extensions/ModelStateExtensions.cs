using System;
using System.Collections.Generic;
using Light.Oaks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// Mvc extensions.
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Adds the invalid model state exception.
        /// </summary>
        /// <param name="options">Options.</param>
        public static void UseInvalidModelStateException(this ApiBehaviorOptions options)
        {
            //options.SuppressModelStateInvalidFilter = false;
            options.InvalidModelStateResponseFactory += (ActionContext arg) => {
                string msg = null;
                var state = arg.ModelState;
                ErrorData[] errors = null;
                var ie = state as IEnumerable<KeyValuePair<string, ModelStateEntry>>;
                errors = new ErrorData[state.ErrorCount];
                int i = 0;
                foreach (var item in ie) {
                    if (msg == null) {
                        msg = $"{item.Key}:{item.Value.Errors[0].ErrorMessage}";
                    }
                    var count = item.Value.Errors.Count;
                    var errorMsgs = new string[count];
                    for (int j = 0; j < count; j++) {
                        errorMsgs[j] = item.Value.Errors[j].ErrorMessage;
                    }
                    var info = string.Join(';', errorMsgs);

                    var data = new ErrorData() {
                        Name = item.Key,
                        Info = info
                    };
                    errors[i] = data;
                    i++;
                }

                throw new ParameterException(msg, errors);
            };
        }
    }
}
