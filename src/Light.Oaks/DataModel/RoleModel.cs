using System;
using System.Collections;
using System.Collections.Generic;
namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleModel : IEnumerable<string>
    {
        readonly HashSet<string> hash = new HashSet<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permissions"></param>
        public RoleModel(string role, ICollection<string> permissions)
        {
            if (permissions == null) {
                throw new ArgumentNullException(nameof(permissions));
            }
            this.Role = role ?? throw new ArgumentNullException(nameof(role));
            foreach (var item in permissions) {
                hash.Add(item);
                IsAdmin |= item == "admin";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAdmin { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Role { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool CheckPermission(string permission)
        {
            return hash.Contains(permission);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return hash.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return hash.GetEnumerator();
        }
    }


}
