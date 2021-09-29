using System;
using System.Diagnostics;
using System.IO;
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
            Thread.Sleep(10);
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            Thread.Sleep(10);
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
            Foo foo1 = new Foo(tracer);
            foo1.MyMethod();
            thread.Start(tracer);
            thread.Join();
            TraceResult traceResult = tracer.GetTraceResult();
            ISerialize XML = new XMLSerialize();
            string xml = XML.serialize(traceResult); 
            Console.WriteLine(xml);
            File.WriteAllText("C://Users//kiril//OneDrive//Рабочий стол//Учеба//3 курс//СПП//trace.xml", xml);
            Console.ReadKey();
        }
    }
}
