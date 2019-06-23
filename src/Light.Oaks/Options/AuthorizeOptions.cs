using System;
namespace Light.Oaks
{
    public class AuthorizeOptions
    {
        public string TokenName { get; set; }

        public int Expiry { get; set; }

        public bool TestMode { get; set; }
    }
}
