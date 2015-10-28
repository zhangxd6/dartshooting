using Microsoft.ServiceFabric.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountStateful.Contracts
{
    public interface  ICount:IService
    {
        Task<long> GetCount();
    }
}
