using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services;
using CountStateful.Contracts;

namespace CountStateful
{
    public class CountStateful : StatefulService,ICount

    {
        public  async Task<long> GetCount()
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("dartCount");
            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, "Counter");
                ServiceEventSource.Current.ServiceMessage(
                    this,
                    "Current Counter Value: {0}",
                    result.HasValue ? result.Value.ToString() : "Value does not exist.");
                if (result.HasValue)
                {
                    return await Task.FromResult<long>(result.Value);
                }
                else
                {
                    return await Task.FromResult<long>(0);
                }

            }

        }

        protected override ICommunicationListener CreateCommunicationListener()
        {
            // TODO: Replace this with an ICommunicationListener implementation if your service needs to handle user requests.
            return new ServiceCommunicationListener<ICount>(this);
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("dartCount");

            while (!cancellationToken.IsCancellationRequested)
            {
                using (var tx = this.StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");
                    ServiceEventSource.Current.ServiceMessage(
                        this,
                        "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (k, v) => ++v);

                    await tx.CommitAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
