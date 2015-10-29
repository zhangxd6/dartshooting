using CountStateful.Contracts;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Services;
using StatelessActor.Interfaces;
using StatelessActor.Interfaces.Models;
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


        [HttpGet]
        [Route("~/api/position")]
        public Position GetPosition()
        {
            var actorId = ActorId.NewId();
            var actorAddress = new Uri("fabric:/DartShooting/StatelessActorService");
            IStatelessActor proxy = ActorProxy.Create<IStatelessActor>(actorId, actorAddress);
            return proxy.GetRandomPosition().Result;
        }
    }
}
