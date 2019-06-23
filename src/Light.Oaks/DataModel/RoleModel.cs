using System;
using System.Collections;
using System.Collections.Generic;
namespace Light.Oaks
{
    public class RoleModel : IEnumerable<string>
    {
        readonly HashSet<string> hash = new HashSet<string>();

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

        public bool IsAdmin { get; }

        public string Role { get; }

        public bool CheckPermission(string permission)
        {
            return hash.Contains(permission);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return hash.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return hash.GetEnumerator();
        }
    }


}
