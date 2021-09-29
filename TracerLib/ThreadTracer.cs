using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TracerLib
{
    public class ThreadTracer : ITracer
    {
        public int threadId { get; private set; }
        private Stack<MethodTracer> methodTracers = new Stack<MethodTracer>();
        private MethodTracer currentMethodTracer;
        private List<MethodInfo> methodInfoList = new List<MethodInfo>();

        public TraceResult GetTraceResult()
        {
            throw new NotImplementedException();
        }


        public ThreadTracer(int threadId)
        {
            this.threadId = threadId;
        }

        public List<MethodInfo> GetThreadMethodList()
        {
            return methodInfoList;
        }

        public void StartTrace()
        {
            if (currentMethodTracer != null)
            {
                methodTracers.Push(currentMethodTracer);
            }
            currentMethodTracer = new MethodTracer();
            currentMethodTracer.StartTrace();
        }

        public void StopTrace()
        {
            currentMethodTracer.StopTrace();
            StackTrace stackTrace = new StackTrace();
            string methodName = stackTrace.GetFrame(2).GetMethod().Name;
            string className = stackTrace.GetFrame(2).GetMethod().ReflectedType.Name;
            long methodExecutionTime = currentMethodTracer.getExecutionTime();
            List<MethodInfo> methodInfoList = currentMethodTracer.getChildMethods();
            MethodInfo methodInfo = new MethodInfo();
            methodInfo.setName(methodName);
            methodInfo.setClassName(className);
            methodInfo.setExecutionTime(methodExecutionTime);
            methodInfo.setChildMethods(methodInfoList);

            if (methodTracers.Count > 0)
            {
                // turn again to previous method
                currentMethodTracer = methodTracers.Pop();
                currentMethodTracer.addChildMethod(methodInfo);
                // CurrentMethodTracer.StartTrace();
            }
            else
            {
                methodInfoList.Add(methodInfo);
                currentMethodTracer = null;
            }
        }
    }
}
