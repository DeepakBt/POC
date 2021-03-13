using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDB
{
    public interface IMyRep
    {
        List<MyData> GetAllEmployees();
    }
}
