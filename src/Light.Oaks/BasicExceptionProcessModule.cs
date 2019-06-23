using System;
using System.Collections.Generic;

namespace Light.Oaks
{
    class BasicExceptionProcessModule : IExceptionProcessModule
    {
        readonly List<ExceptonTypeModel> typeModels;

        readonly bool useOkStatus;

        readonly int defaultHttpStatus;

        public BasicExceptionProcessModule(ExceptionOptions options)
        {
            defaultHttpStatus = options.UseOkStatus ? 200 : 400;
            useOkStatus = options.UseOkStatus;
            var baselist = new List<ExceptonTypeModel> {
                new ExceptonTypeModel(typeof(AuthorizeException), (exception) => {
                    var typeEx = (AuthorizeException)exception;
                    var model = new ExceptionModel() {
                        Code = 40100,
                        Message = typeEx.Message,
                        LogType = LogType.LogMessage
                    };
                    if (!useOkStatus) {
                        model.HttpStatus = 401;
                    }
                    return model;
                }),

                new ExceptonTypeModel(typeof(PermissionException), (exception) => {
                    var typeEx = (PermissionException)exception;
                    var model = new ExceptionModel() {
                        Code = 40300,
                        Message = typeEx.Message,
                        LogType = LogType.LogMessage
                    };
                    if (!useOkStatus) {
                        model.HttpStatus = 403;
                    }
                    return model;
                }),

                new ExceptonTypeModel(typeof(ParameterException), (exception) => {
                    var typeEx = (ParameterException)exception;
                    var model = new ExceptionModel() {
                        Code = 40000,
                        Message = typeEx.Message,
                        LogType = LogType.None,
                        Errors = typeEx.Errors
                    };
                    return model;
                }),

                new ExceptonTypeModel(typeof(Exception), (exception) => {
                    var model = new ExceptionModel() {
                        Code = 50000,
                        Message = exception.Message,
                        LogType = LogType.LogFullException | LogType.LogPostData | LogType.LogTraceId
                    };
                    if (exception is IMultiError multi) {
                        model.Errors = multi.Errors;
                    }
                    return model;
                })
            };

            typeModels = Combine(baselist, options.ExceptionTypes);

        }

        List<ExceptonTypeModel> Combine(List<ExceptonTypeModel> baselist, List<ExceptonTypeModel> list)
        {
            if (list == null) {
                return baselist;
            }
            for (int i = 0; i < list.Count; i++) {
                var typeModel = list[i];
                bool flag = false;
                for (int j = 0; j < baselist.Count; j++) {
                    var typeModel1 = baselist[j];
                    if (typeModel.ExceptionType == typeModel1.ExceptionType) {
                        baselist[j] = typeModel;
                        flag = true;
                        break;
                    }
                    else if (typeModel.ExceptionType.IsSubclassOf(typeModel1.ExceptionType)) {
                        baselist.Insert(j, typeModel);
                        flag = true;
                        break;
                    }
                }
                if (!flag) {
                    baselist.Add(typeModel);
                }
            }
            return baselist;
        }




        public ExceptionModel ProcessException(Exception exception)
        {
            ExceptionModel result = null;
            Type type = exception.GetType();
            foreach (var typeModel in typeModels) {
                if (type == typeModel.ExceptionType || type.IsSubclassOf(typeModel.ExceptionType)) {
                    result = typeModel.ExceptionFunc.Invoke(exception);
                    break;
                }
            }
            if (result == null) {
                result = new ExceptionModel() {
                    Code = 50000,
                    Message = exception.Message,
                    LogType = LogType.LogFullException | LogType.LogPostData | LogType.LogTraceId
                };
                if (exception is IMultiError multi) {
                    result.Errors = multi.Errors;
                }
            }

            if (result.HttpStatus < 200) {
                result.HttpStatus = defaultHttpStatus;
            }
            return result;
        }
    }
}
