using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StatefulActor.Interfaces;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Actors;
using StatefulActor.Interfaces.Models;

namespace StatefulActor
{
    public class StatefulActor : Actor<StatefulActorState>, IStatefulActor
    {
        public override Task OnActivateAsync()
        {
            if (this.State == null)
            {
                this.State = new StatefulActorState() { Count = 0 };
            }

            ActorEventSource.Current.ActorMessage(this, "State initialized to {0}", this.State);
            return Task.FromResult(true);
        }

        public async Task<int> GetCountAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Generate Random position at partition " + this.Host.StatefulServiceInitializationParameters.PartitionId + "and instance id " + Host.StatefulServiceInitializationParameters.ReplicaId);

            ActorEventSource.Current.ActorMessage(this, "Getting current count value as {0}", this.State.Count);
            return await Task.FromResult(this.State.Count);
        }

        private async Task<bool> SetCountAsync(int count)
        {
            ActorEventSource.Current.ActorMessage(this, "Generate Random position at partition " + this.Host.StatefulServiceInitializationParameters.PartitionId + "and instance id " + Host.StatefulServiceInitializationParameters.ReplicaId);

            ActorEventSource.Current.ActorMessage(this, "Setting current count of value to {0}", count);
            this.State.Count = count;

            return await Task.FromResult(true);
        }

        public async Task<Location> GetRandomPosition()
        {
            ActorEventSource.Current.ActorMessage(this, "Generate Random position at partition " + this.Host.StatefulServiceInitializationParameters.PartitionId + "and instance id " + Host.StatefulServiceInitializationParameters.ReplicaId);

            Random rnd = new Random(DateTime.UtcNow.Millisecond);
            Location p = new Location()
            {
                X = rnd.Next(0, 400),
                Y = rnd.Next(0, 800)
            };
            int count = await GetCountAsync();
            await SetCountAsync(count+1);
            ActorEventSource.Current.ActorMessage(this, string.Format("new postion position ({0},{1})", p.X, p.Y));
            return await Task.FromResult<Location>(p);
        }
    }
}
