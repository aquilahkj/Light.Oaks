using System;
using System.ComponentModel.DataAnnotations;

namespace Light.Oaks.Demo.Model
{
    public class LoginResult
    {
        /// <summary>
        /// 登录结果 0: 失败 1:成功 2:已在别处登录
        /// </summary>
        /// <value>The result.</value>
        public int Result { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        /// <value>The reason.</value>
        public string Message { get; set; }

        ///// <summary>
        ///// 最后登录时间
        ///// </summary>
        ///// <value>The latest login time.</value>
        //public DateTime? LatestLoginTime { get; set; }

        /// <summary>
        /// 授权令牌
        /// </summary>
        /// <value>The token.</value>
        public string Token { get; set; }
    }
}