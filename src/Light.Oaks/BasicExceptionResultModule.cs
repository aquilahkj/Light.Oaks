using System;
namespace Light.Oaks
{
    public class BasicExceptionResultModule : IExceptionResultModule
    {
        public object CreateResult(Exception exception, ErrorResult result)
        {
            return result;
        }
    }


}
