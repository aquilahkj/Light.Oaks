using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Light.Oaks
{
    class AuthorizeManagement : IAuthorizeManagement
    {
        readonly IPermissionModule permissionModule;
        readonly IAuthorizeModule authorizeModule;

        Dictionary<string, RoleModel> rolePermission;
        readonly object roleLocker = new object();

        public AuthorizeManagement(IServiceProvider serviceProvider)
        {
            permissionModule = serviceProvider.GetRequiredService<IPermissionModule>();
            authorizeModule = serviceProvider.GetRequiredService<IAuthorizeModule>();
            if(!string.IsNullOrEmpty(authorizeModule.ToeknName)) {
                TokenName = authorizeModule.ToeknName;
            }
            else {
                TokenName = "X-Token";
            }
        }

        public string TokenName { get; }

        public AuthorizeInfo VerifyAuthorize(string token)
        {
            var verifyInfo = authorizeModule.VerifyToken(token);
            var authorizeInfo = new AuthorizeInfo() {
                Id = verifyInfo.Id,
                Account = verifyInfo.Account,
                CreateTime = verifyInfo.CreateTime,
                Key = verifyInfo.Key,
                Name = verifyInfo.Name,
                Roles = GetRoleCollection(verifyInfo.Roles)
            };
            return authorizeInfo;
        }

        private RoleCollection GetRoleCollection(string[] roles)
        {
            var dict = GetRoleModels();
            var list = new List<RoleModel>();
            foreach (var role in roles) {
                if (dict.TryGetValue(role, out RoleModel mode)) {
                    list.Add(mode);
                }
            }
            return new RoleCollection(list);
        }

        private Dictionary<string, RoleModel> GetRoleModels()
        {
            var dict = rolePermission;
            if (dict == null) {
                lock (roleLocker) {
                    dict = rolePermission;
                    if (dict == null) {
                        var mydict = new Dictionary<string, List<string>>();
                        var array = permissionModule.GetRolePermissions();
                        foreach (var item in array) {
                            if (!mydict.TryGetValue(item.Role, out List<string> list)) {
                                list = new List<string>();
                                mydict.Add(item.Role, list);
                            }
                            list.Add(item.PermissionCode);
                        }
                        var newdict = new Dictionary<string, RoleModel>();
                        foreach (var kvs in mydict) {
                            newdict.Add(kvs.Key, new RoleModel(kvs.Key, kvs.Value));
                        }
                        rolePermission = newdict;
                        dict = rolePermission;
                    }
                }
            }
            return dict;
        }

        public void ResetRolePermission()
        {
            lock (roleLocker) {
                rolePermission = null;
            }
        }
    }
}
