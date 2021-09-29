using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public interface ISerialize
    {
        string serialize(TraceResult traceResult);
    }
}
