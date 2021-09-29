using System;
using System.Diagnostics;
using System.Threading;
using TracerLib;

namespace ConsoleApp
{
    class Program
    {

        public void startTracerInNewThread(object o)
        {
            Tracer tracer = (Tracer)o;
            tracer.StartTrace();
            Thread.Sleep(1000);
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            tracer.StopTrace();
        }
        static void Main()
        {
            Tracer tracer = new Tracer();
            Program program = new Program();
            Thread thread = new Thread(new ParameterizedThreadStart(program.startTracerInNewThread));
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            foo.MyMethod();
            thread.Start(tracer);
            thread.Join();
            TraceResult traceResult = tracer.GetTraceResult();
            //Console.WriteLine(stackTrace.GetFrame(0).GetMethod().Name);
            Console.ReadKey();
        }
    }
}
