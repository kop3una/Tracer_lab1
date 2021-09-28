using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TracerLib
{
    public class MethodTracer : ITracer
    {
        private Stopwatch stopwatch = new Stopwatch();
        private List<MethodInfo> methodInfoList = new List<MethodInfo>();

        public void addChildMethod(MethodInfo childMethod)
        {
            methodInfoList.Add(childMethod);
        }

        public List<MethodInfo> getChildMethods()
        {
            return methodInfoList;
        }

        public TraceResult GetTraceResult()
        {
            throw new NotImplementedException();
        }

        public void StartTrace()
        {
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
        }
    }
}
