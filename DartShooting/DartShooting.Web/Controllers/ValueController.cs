using CountStateful.Contracts;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Services;
using StatefulActor.Interfaces;
using StatelessActor.Interfaces;
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
        public StatelessActor.Interfaces.Models.Position GetPosition()
        {
            var actorId = new ActorId(11111);
            var actorAddress = new Uri("fabric:/DartShooting/StatelessActorService");
            IStatelessActor proxy = ActorProxy.Create<IStatelessActor>(actorId, actorAddress);
            return proxy.GetRandomPosition().Result;
        }

        [HttpGet]
        [Route("~/api/state/position")]
        public StatefulActor.Interfaces.Models.Location GetStatefulPosition()
        {
            var actorId = new ActorId(11111);

            var actorAddress = new Uri("fabric:/DartShooting/StatefulActorService");
            IStatefulActor proxy = ActorProxy.Create<IStatefulActor>(actorId, actorAddress);
            return proxy.GetRandomPosition().Result;
        }

        [HttpGet]
        [Route("~/api/state/position/count")]
        public int GetStatefulPositionCount()
        {
            var actorId = new ActorId(11111);
            var actorAddress = new Uri("fabric:/DartShooting/StatefulActorService");
            IStatefulActor proxy = ActorProxy.Create<IStatefulActor>(actorId, actorAddress);
            return proxy.GetCountAsync().Result;
        }
    }
}