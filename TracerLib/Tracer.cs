using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        private List<ThreadTracer> threadTracersList;
        private List<ThreadInfo> threadsInfoList;
        static object locker = new object();
        public Tracer()
        {
            threadsInfoList = new List<ThreadInfo>();
            threadTracersList = new List<ThreadTracer>();
        }


        public TraceResult GetTraceResult()
        {
            TraceResult traceResult = new TraceResult();
            traceResult.threadInfoList = this.threadsInfoList;
            return traceResult;
        }

        private ThreadInfo GetThreadInfoById(int threadId)
        {
            lock (locker)
            {
                foreach (ThreadInfo threadInfo in threadsInfoList)
                {
                    if (threadId == threadInfo.id)
                    {
                        return threadInfo;
                    }
                }
            }
            return null;
        }

        private ThreadTracer GetCurrentThreadTracer()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            lock (locker)
            {
                foreach (ThreadTracer threadTracer in threadTracersList)
                {
                    if (threadId == threadTracer.threadId)
                    {
                        return threadTracer;
                    }
                }
            }
            return null;
        }

        public void StartTrace()
        {
            ThreadTracer threadTracer = GetCurrentThreadTracer();
            if (threadTracer == null)
            {
                int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                threadTracer = new ThreadTracer(currentThreadId);
                lock (locker)
                {
                    threadTracersList.Add(threadTracer);
                }

            }
            threadTracer.StartTrace();
        }

        public void StopTrace()
        {
            ThreadTracer threadTracer = GetCurrentThreadTracer(); // check
            threadTracer.StopTrace();

            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            ThreadInfo threadInfo = GetThreadInfoById(currentThreadId);
            if (threadInfo == null)
            {
                List<MethodInfo> threadMethodInfos = threadTracer.GetMethodInfoList();
                threadInfo = new ThreadInfo(currentThreadId, threadMethodInfos);
                lock (locker)
                {
                    threadsInfoList.Add(threadInfo);
                }
            }
        }
    }
}
