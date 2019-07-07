using System;
using System.Collections;
using System.Collections.Generic;

namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleCollection : IReadOnlyCollection<RoleModel>
    {
        readonly List<RoleModel> list = new List<RoleModel>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        public RoleCollection(ICollection<RoleModel> roles)
        {
            if (roles != null)
                list.AddRange(roles);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count => list.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<RoleModel> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetRoles()
        {
            var hash = new HashSet<string>();
            foreach (var item in list) {
                hash.Add(item.Role);
            }
            var array = new string[hash.Count];
            hash.CopyTo(array);
            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetPermissions()
        {
            var hash = new HashSet<string>();
            foreach (var item in list) {
                foreach (var permission in item) {
                    hash.Add(permission);
                }
            }
            var array = new string[hash.Count];
            hash.CopyTo(array);
            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool CheckPermission(string permission)
        {
            foreach (var item in list) {
                if (item.IsAdmin) {
                    return true;
                }
                if (item.CheckPermission(permission)) {
                    return true;
                }
            }
            return false;
        }
    }
}
