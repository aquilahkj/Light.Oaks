using System;
namespace Light.Oaks.Demo
{
    public class CustomizeException : Exception
    {
        public CustomizeException()
        {
        }

        public CustomizeException(string message) : base(message)
        {
        }
    }
}
