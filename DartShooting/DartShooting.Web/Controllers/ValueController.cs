using CountStateful.Contracts;
using Microsoft.ServiceFabric.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DartShooting.Web.Controllers
{
    [RoutePrefix("api/value")]
    public class ValueController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "Hello World";
        }

        [HttpGet]
        [Route("~/api/count")]
        public long GetCount()
        {
            var proxy = ServiceProxy.Create<ICount>("999977808", new Uri("fabric:/DartShooting/CountStateful"));
            return proxy.GetCount().Result;
        } 
    }
}
