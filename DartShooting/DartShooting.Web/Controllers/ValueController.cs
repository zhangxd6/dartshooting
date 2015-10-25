using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DartShooting.Web.Controllers
{
    [RoutePrefix("api/value1")]
    public class ValueController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "Hello World";
        }
    }
}
