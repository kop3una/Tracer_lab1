using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TracerLib
{
    class Tracer : ITracer
    {
        private List<ThreadTracer> ThreadTracersList;
        private List<ThreadInfo> ThreadsInfoList;
        static object Locker = new object();
        public Tracer()
        {
            ThreadsInfoList = new List<ThreadInfo>();
            ThreadTracersList = new List<ThreadTracer>();
        }


        public TraceResult GetTraceResult()
        {
            TraceResult traceResult = new TraceResult();
            traceResult.threadInfoList = this.ThreadsInfoList;
            return traceResult;
        }

        private ThreadInfo GetThreadInfoById(int threadId)
        {
            lock (Locker)
            {
                foreach (ThreadInfo threadInfo in ThreadsInfoList)
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
            lock (Locker)
            {
                foreach (ThreadTracer threadTracer in ThreadTracersList)
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
                lock (Locker)
                {
                    ThreadTracersList.Add(threadTracer);
                }

            }
            threadTracer.StartTrace();
        }

        public void StopTrace()
        {
            ThreadTracer threadTracer = GetCurrentThreadTracer();
            threadTracer.StopTrace();
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            ThreadInfo threadInfo = GetThreadInfoById(currentThreadId);
            if (threadInfo == null)
            {
                List<MethodInfo> threadMethodInfos = threadTracer.GetThreadMethodList();
                threadInfo = new ThreadInfo(currentThreadId, threadMethodInfos);
                lock (Locker)
                {
                    ThreadsInfoList.Add(threadInfo);
                }
            }
        }
    }
}
