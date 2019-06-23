using System;
namespace Light.Oaks
{
    public interface IExceptionResultModule
    {
        object CreateResult(Exception exception, ErrorResult result);
    }
}
