using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class Foo
    {
        private Bar bar;
        private ITracer tracer;

        public Foo(ITracer tracer)
        {
            this.tracer = tracer;
            bar = new Bar(tracer);
        }

        public void MyMethod()
        {
            tracer.StartTrace();
            bar.InnerMethod();
            tracer.StopTrace();
        }
    }
}
