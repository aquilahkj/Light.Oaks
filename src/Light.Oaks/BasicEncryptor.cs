using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Light.Oaks
{
    class BasicEncryptor : IEncryptor
    {
        private static byte[] Ivs = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        DESCryptoServiceProvider des;

        public BasicEncryptor()
        {
            var key = Assembly.GetCallingAssembly().FullName;

            des = new DESCryptoServiceProvider();
            var buffer = Encoding.UTF8.GetBytes(key);
            using (var md5 = MD5.Create()) {
                buffer = md5.ComputeHash(buffer);
            }
            byte[] keyBytes = new byte[8];
            Buffer.BlockCopy(buffer, 4, keyBytes, 0, 8);
            des.IV = Ivs;
            des.Key = keyBytes;
        }

        public string Decrypt(string content)
        {
            byte[] buffer = Convert.FromBase64String(content);
            ICryptoTransform decrypt = des.CreateDecryptor();
            byte[] result = decrypt.TransformFinalBlock(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(result);
        }

        public string Encrypt(string content)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            ICryptoTransform encrypt = this.des.CreateEncryptor();
            byte[] result = encrypt.TransformFinalBlock(buffer, 0, buffer.Length);
            return Convert.ToBase64String(result);
        }
    }
}
