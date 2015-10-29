using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using StatelessActor.Interfaces.Models;

namespace StatelessActor.Interfaces
{
    public interface IStatelessActor : IActor
    {
        Task<Position> GetRandomPosition();
    }
}
