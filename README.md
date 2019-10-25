# Light.Oaks

`Light.Oaks`是一个集成鉴权验证和异常处理的WebApi扩展

## 鉴权

Http Request的Header中需要携带X-Token，Token由系统颁发。
在`Controller`中的方法中加入`[AuthorizePermission]`Attribute，参数为权限编码，为空则任意权限均可访问。

```csharp
	[Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [AuthorizePermission]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [AuthorizePermission("read")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [AuthorizePermission("create")]
        public ActionResult<int> Post([FromBody] string value)
        {
            return 1;
        }
    }
```

在配置方法中AddAuthorize即可使用

```csharp
services.AddAuthorize(builder => {
        builder.UseDesEncryptor("aabb");//Token加密密钥
        builder.UseRedisCache("redis_connection");//使用redis
        builder.SetPermissionModule<PermissionModule>(ServiceLifetime.Scoped); //设置角色权限接口
       });
```

如需要自定义角色权限，需要实现`IPermissionModule`接口，并在`AddAuthorize`中设置

```csharp
public interface IPermissionModule
{
	/// <summary>
	///
	/// </summary>
	/// <returns></returns>
	Role[] GetRoles ();
}

```


登录模块中需要依赖注入`IAuthorizeModule`接口

```csharp
	public AuthorizeController(IAuthorizeModule authorizeModule)
    {
        this.authorizeModule = authorizeModule;
    }
```
登录成功后使用`IAuthorizeModule`生成Token，返回客户端

```csharp
	var info = new VerifyInfo() {
              Account = "admin", //帐号
              CreateTime = DateTime.Now,
              Id = "1001", //用户Id
              Name = "admin", //用户名
              Roles = new string[] { “admin” } //用户角色
             };
   var token = authorizeModule.CreateAuthorization(info);
```

## 异常处理

WebApi执行过程中遇到异常，会返回json格式的异常信息。

在配置方法中AddException即可使用，自定义各种异常返回信息。

```csharp
			services.AddException(builder => {
                builder.EnableLogger = true; //异常信息记录日志
                builder.UseOkStatus = true; //产生异常后Http Status Code是否为200
                builder.RegisterException<AuthorizeException>(x => {
                    return new ExceptionModel() {
                        Code = 40101,
                        Message = x.Type.ToString() + ":" + x.Message,
                        HttpStatus = 401,
                        LogType = LogType.LogTraceId | LogType.LogPostData | LogType.LogFullException
                    };
                }); // 自定义内容
                builder.RegisterException<Exception>(50001, "神秘错误", 500, LogType.LogAll); // 默认异常返回
                builder.RegisterException<PermissionException>(40301, httpStatus: 403); //针对指定异常类型返回错误信息
                builder.RegisterException<SubPermissionException>(40302, httpStatus: 403, logType: LogType.LogTraceId); // 定义子类型
                builder.RegisterException<CustomizeException>(41000, "自定义错误", 500, LogType.LogTraceId | LogType.LogFullException);
                builder.RegisterException<ParameterException>(40001, httpStatus: 400, logType: LogType.LogTraceId | LogType.LogPostData);
                builder.RegisterException<SomeException>(40002, httpStatus: 400, logType: LogType.LogTraceId | LogType.LogPostData);
            });
```
