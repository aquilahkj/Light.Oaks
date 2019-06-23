using System;
using Newtonsoft.Json;

namespace Light.Oaks
{
    public class BasicAuthorizeModule : IAuthorizeModule
    {
        const string USER_PREFIX = "UA";

        readonly ICacheAgent cacheAgent;
        readonly IEncryptor encryptor;
        readonly bool testMode;
        readonly TimeSpan expiry;

        public BasicAuthorizeModule(AuthorizeOptions options, ICacheAgent cacheAgent, IEncryptor encryptor)
        {
            this.encryptor = encryptor;
            this.cacheAgent = cacheAgent;
            this.testMode = options.TestMode;
            this.expiry = new TimeSpan(0, options.Expiry > 0 ? options.Expiry : 30, 0);
        }

        public string ToeknName { get; }

        public void RemoveAuthorize(string id)
        {
            cacheAgent.RemoveCache($"{USER_PREFIX}_{id}");
        }

        public bool CheckAuthorize(string id)
        {
            var data = cacheAgent.GetCache($"{USER_PREFIX}_{id}");
            return !string.IsNullOrEmpty(data);
        }

        public string CreateAuthorization(VerifyInfo verifyInfo)
        {
            if (verifyInfo == null) {
                throw new ArgumentNullException(nameof(verifyInfo));
            }
            var cacheData = JsonConvert.SerializeObject(verifyInfo);
            var data = $"{verifyInfo.Id}|{verifyInfo.Key}|{DateTime.Now.ToString("yyyy-MM-DDTHH:mm:ss")}";
            var token = encryptor.Encrypt(data);
            cacheAgent.SetCache($"{USER_PREFIX}_{verifyInfo.Id}", cacheData, expiry);
            return token;
        }

        public VerifyInfo VerifyToken(string token)
        {
            if (string.IsNullOrEmpty(token)) {
                if (testMode) {
                    var info = new VerifyInfo() {
                        Id = "0",
                        Account = "test",
                        CreateTime = DateTime.Now,
                        Key = string.Empty,
                        Name = "test",
                        Roles = new string[0]
                    };
                    return info;
                }
                else {
                    throw new AuthorizeException(AuthorizeErrorType.TokenNotExists, SR.TokenNotExists);
                }
            }

            string data;
            string id;
            string key;
            try {
                data = encryptor.Decrypt(token);
                string[] values = data.Split('|');
                id = values[0];
                key = values[1];
            }
            catch (Exception ex) {
                throw new AuthorizeException(AuthorizeErrorType.TokenError, SR.TokenError, ex);
            }

            var result = cacheAgent.GetCache($"{USER_PREFIX}_{id}");
            if (result == null) {
                throw new AuthorizeException(AuthorizeErrorType.AccountNotLogin, SR.AccountNotLogin);
            }
            var verifyInfo = JsonConvert.DeserializeObject<VerifyInfo>(result);
            if (verifyInfo.Key != key) {
                throw new AuthorizeException(AuthorizeErrorType.AccountHasLoginElsewhere, SR.AccountHasLoginElsewhere);
            }
            return verifyInfo;
        }
    }
}
