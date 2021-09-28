using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    class Bar
    {
        private ITracer tracer;

        internal Bar(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public void InnerMethod()
        {
            int counter = 0;
            tracer.StartTrace();
            while (counter <= 0)
            {
                counter++;
            }
            tracer.StopTrace();
        }
    }
}
