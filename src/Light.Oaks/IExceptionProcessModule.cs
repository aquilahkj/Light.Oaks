using System;
namespace Light.Oaks
{
    public interface IExceptionProcessModule
    {
        //bool UseOkStatus { get; }

        //bool RespOkStatus { get; }

        ExceptionModel ProcessException(Exception ex);
    }
}
