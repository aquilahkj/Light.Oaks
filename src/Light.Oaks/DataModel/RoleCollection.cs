using System;
using System.Collections;
using System.Collections.Generic;

namespace Light.Oaks
{
    public class RoleCollection : IReadOnlyCollection<RoleModel>
    {
        readonly List<RoleModel> list = new List<RoleModel>();

        public RoleCollection(ICollection<RoleModel> roles)
        {
            list.AddRange(roles);
        }

        public int Count => list.Count;

        public IEnumerator<RoleModel> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

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

        public string[] GetPermissions()
        {
            var hash = new HashSet<string>();
            foreach (var item in list) {
                foreach(var permission in item) {
                    hash.Add(permission);
                }
            }
            var array = new string[hash.Count];
            hash.CopyTo(array);
            return array;
        }


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
