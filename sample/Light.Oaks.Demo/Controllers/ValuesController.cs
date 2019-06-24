using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Light.Oaks.Demo.Controllers
{
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

        // PUT api/values/5
        [HttpPut("{id}")]
        [AuthorizePermission("update")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [AuthorizePermission("delete")]
        public void Delete(int id)
        {
        }

        [HttpGet("startup")]
        [AuthorizePermission("startup")]
        public string Startup()
        {
            return "ok";
        }

        [HttpGet("custom-error")]
        [AuthorizePermission]
        public string CustomizeError()
        {
            throw new CustomizeException("some wrong");
        }

        [HttpGet("some-error")]
        [AuthorizePermission]
        public string SomeError()
        {
            throw new SomeException("some wrong");
        }

        [HttpGet("not-impl")]
        [AuthorizePermission]
        public string NotImplemented()
        {
            throw new NotImplementedException("not implement");
        }

        [HttpGet("sub-permission")]
        [AuthorizePermission]
        public string NoPermission()
        {
            throw new SubPermissionException("sub permissionn\nTest");
        }
    }
}
