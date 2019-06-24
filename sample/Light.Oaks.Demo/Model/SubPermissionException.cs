using System;
namespace Light.Oaks.Demo
{
    public class SubPermissionException : PermissionException
    {
        public SubPermissionException(string message) : base(message)
        {
        }
    }
}
