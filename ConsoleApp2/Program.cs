using System;
using System.Diagnostics;
using TracerLib;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            MethodTracer methodTracer = new MethodTracer();
            Foo foo = new Foo(methodTracer);
            foo.MyMethod();

            Console.WriteLine("Hello World!");
            StackTrace stackTrace = new StackTrace();
            // Get calling method name

            Console.WriteLine(stackTrace.GetFrame(0).GetMethod().Name);
            Console.ReadKey();
        }
    }
}
