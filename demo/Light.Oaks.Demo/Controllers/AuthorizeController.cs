using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Light.Oaks.Demo.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Light.Oaks.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : Controller
    {
        private class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        Dictionary<string, User> dict = new Dictionary<string, User>();

        readonly IAuthorizeModule authorizeModule;

        public AuthorizeController(IAuthorizeModule authorizeModule)
        {
            this.authorizeModule = authorizeModule;

            dict.Add("admin1", new User() { Id = 1, Name = "admin1", Password = "123456", Role = "admin" });
            dict.Add("user1", new User() { Id = 2, Name = "user1", Password = "123456", Role = "user" });
            dict.Add("guest1", new User() { Id = 3, Name = "guest1", Password = "123456", Role = "guest" });

        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="model">Model.</param>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResult), 200)]
        public LoginResult Login([FromBody]LoginModel model)
        {
            var result = new LoginResult();
            if (dict.TryGetValue(model.Account, out User user)) {
                if (user.Password == model.Password) {
                    var info = new VerifyInfo() {
                        Account = user.Name,
                        CreateTime = DateTime.Now,
                        Id = user.Id.ToString(),
                        Key = Guid.NewGuid().ToString("N"),
                        Name = model.Account,
                        Roles = new string[] { user.Role }
                    };
                    var token = authorizeModule.CreateAuthorization(info);
                    result.Token = token;
                    result.Result = 1;
                    result.Message = "ok";
                }
                else {
                    result.Result = 0;
                    result.Message = "password error";
                }

            }
            else {
                result.Result = 0;
                result.Message = "user error";
            }
            return result;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns>The login.</returns>
        [HttpPost("logout")]
        [AuthorizePermission]
        [ProducesResponseType(typeof(LoginResult), 200)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        public SuccessResult Logout()
        {
            var id = HttpContext.GetUserId();
            authorizeModule.RemoveAuthorize(id);
            return new SuccessResult();
        }
    }
}
