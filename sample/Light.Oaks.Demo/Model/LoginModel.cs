using System;
using System.ComponentModel.DataAnnotations;

namespace Light.Oaks.Demo.Model
{
    public class LoginModel
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        /// <value>The account.</value>
        [Required()]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        /// <value>The password.</value>
        [Required]
        public string Password { get; set; }
    }
}
