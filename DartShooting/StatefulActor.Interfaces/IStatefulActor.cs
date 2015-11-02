using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using StatefulActor.Interfaces.Models;

namespace StatefulActor.Interfaces
{
    public interface IStatefulActor : IActor
    {
        Task<Location> GetRandomPosition();

        Task<int> GetCountAsync();
    }
}
