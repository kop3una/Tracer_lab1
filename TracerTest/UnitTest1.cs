using ConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TracerLib;

namespace TracerTest
{
    [TestClass]
    public class UnitTest1
    {
        private TraceResult ProgrammTraceResult()
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
            return traceResult;
        }
        private TraceResult customTraceResult ()
        {
            List<MethodInfo> methodInfoList = new List<MethodInfo>();
            MethodInfo methodInfo = new MethodInfo();
            methodInfo.className = "MyClass";
            methodInfo.executionTime = 100;
            methodInfo.name = "myMethod";
            methodInfoList.Add(methodInfo);
            methodInfo.childMethods = new List<MethodInfo>();
            ThreadInfo threadInfo = new ThreadInfo(1,methodInfoList);
            List<ThreadInfo> threadInfoList = new List<ThreadInfo>();
            threadInfoList.Add(threadInfo);
            TraceResult traceResult = new TraceResult();
            traceResult.threadInfoList = threadInfoList;
            return traceResult;
        }

        [TestMethod]
        public void TestXML()
        {
            TraceResult traceResult = customTraceResult();
            ISerialize XML = new XMLSerialize();
            string xml = XML.serialize(traceResult);
            string testXml = File.ReadAllText("C://Users//kiril//OneDrive//Рабочий стол//Учеба//3 курс//СПП//lab1//traceTest.xml");
            Assert.IsTrue(xml.Equals(testXml));
        }

        [TestMethod]
        public void TestJSON()
        {
            TraceResult traceResult = customTraceResult();
            ISerialize JSON = new JSONSerialize();
            string json = JSON.serialize(traceResult);
            string testJson = File.ReadAllText("C://Users//kiril//OneDrive//Рабочий стол//Учеба//3 курс//СПП//lab1//traceTest.json");
            Assert.IsTrue(json.Equals(testJson));
        }

        [TestMethod]
        public void checkThreadCount()
        {
            TraceResult traceResult = ProgrammTraceResult();
            List<ThreadInfo> threadInfoList = traceResult.threadInfoList;
            Assert.IsTrue(threadInfoList.Count == 2);
        }

        [TestMethod]
        public void checkMyMethoodCount()
        {
            TraceResult traceResult = ProgrammTraceResult();
            List<ThreadInfo> threadInfoList = traceResult.threadInfoList;
            int checkSum = 0;
            checkSum += threadInfoList[0].methodsList[0].childMethods.Count;
            checkSum += threadInfoList[1].methodsList.Count;
            Assert.IsTrue(checkSum == 4);
        }
        
    }
}
