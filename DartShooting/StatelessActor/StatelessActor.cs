using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StatelessActor.Interfaces;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Actors;
using StatelessActor.Interfaces.Models;

namespace StatelessActor
{
    public class StatelessActor : Actor, IStatelessActor
    {

        public async Task<Position> GetRandomPosition()
        {
            
            ActorEventSource.Current.ActorMessage(this, "Generate Random position at partition "+ this.Host.StatelessServiceInitializationParameters.PartitionId +"and instance id " + Host.StatelessServiceInitializationParameters.InstanceId);
            Random rnd = new Random(DateTime.UtcNow.Millisecond);
            Position p = new Position()
            {
                X = rnd.Next(0, 400),
                Y = rnd.Next(0, 800)
            };
            ActorEventSource.Current.ActorMessage(this, string.Format("new postion position ({0},{1})",p.X,p.Y));
            return await Task.FromResult<Position>(p);

        }
    }
}
