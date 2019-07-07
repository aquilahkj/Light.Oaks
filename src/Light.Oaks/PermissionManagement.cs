using System;
using System.Collections.Generic;

namespace Light.Oaks
{
    class PermissionManagement
    {
        Dictionary<string, RoleModel> rolePermission;

        public bool HasLoad
        {
            get {
                return rolePermission != null;
            }
        }

        //public void SetRolePermissions(RolePermission[] rolePermissions)
        //{
        //    if (rolePermissions == null) {
        //        rolePermission = new Dictionary<string, RoleModel>();
        //        return;
        //    }
        //    var mydict = new Dictionary<string, List<string>>();
        //    foreach (var item in rolePermissions) {
        //        if (!mydict.TryGetValue(item.Role, out List<string> list)) {
        //            list = new List<string>();
        //            mydict.Add(item.Role, list);
        //        }
        //        list.Add(item.Permission);
        //    }
        //    var newdict = new Dictionary<string, RoleModel>();
        //    foreach (var kvs in mydict) {
        //        newdict.Add(kvs.Key, new RoleModel(kvs.Key, kvs.Value));
        //    }
        //    rolePermission = newdict;
        //}


        public void SetRoles(Role[] roles)
        {
            if (roles == null) {
                rolePermission = new Dictionary<string, RoleModel>();
                return;
            }
            var newdict = new Dictionary<string, RoleModel>();
            foreach (var item in roles) {
                newdict[item.Code] = new RoleModel(item.Code, item.Permissions);
            }
            rolePermission = newdict;
        }

        public RoleCollection GetRoleCollection(string[] roles)
        {
            var dict = rolePermission;
            if (dict == null) {
                return new RoleCollection(null);
            }
            var list = new List<RoleModel>();
            foreach (var role in roles) {
                if (dict.TryGetValue(role, out RoleModel mode)) {
                    list.Add(mode);
                }
            }
            return new RoleCollection(list);
        }
    }
}
