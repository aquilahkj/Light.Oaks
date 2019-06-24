using System;
using System.Collections.Generic;

namespace Light.Oaks
{
    /// <summary>
    /// Authorize permission attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorizePermissionAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public AuthorizePermissionAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permission"></param>
        public AuthorizePermissionAttribute(string permission)
        {
            if (string.IsNullOrEmpty(permission)) {
                throw new ArgumentNullException(nameof(permission));
            }
            var hash = new HashSet<string>();
            foreach (var item in permission.Split(',', StringSplitOptions.RemoveEmptyEntries)) {
                var t = item.Trim();
                if (t != string.Empty) {
                    hash.Add(t);
                }
            }
            if (hash.Count > 0) {
                var array = new string[hash.Count];
                hash.CopyTo(array);
                Permission = array;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] Permission { get; }
    }
}
