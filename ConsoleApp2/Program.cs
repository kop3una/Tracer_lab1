using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TracerLib;

namespace ConsoleApp
{
    public class Program
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
            thread.Start(tracer);
            thread.Join();
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            foo.MyMethod();
            Foo foo1 = new Foo(tracer);
            foo1.MyMethod();
            TraceResult traceResult = tracer.GetTraceResult();
            ISerialize XML = new XMLSerialize();
            ISerialize JSON = new JSONSerialize();
            string xml = XML.serialize(traceResult);
            string json = JSON.serialize(traceResult);
            Console.WriteLine(xml);
            Console.WriteLine(json);
            File.WriteAllText("C://Users//kiril//OneDrive//Рабочий стол//Учеба//3 курс//СПП//lab1//trace.xml", xml);
            File.WriteAllText("C://Users//kiril//OneDrive//Рабочий стол//Учеба//3 курс//СПП//lab1//trace.json", json);
            Console.ReadKey();
        }
    }
}
