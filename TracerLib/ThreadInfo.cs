using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    class ThreadInfo
    {
        public int id { get; private set; }
        public List<MethodInfo> methodsList { get; set; }

        private double executionTime;
        public ThreadInfo(int id, List<MethodInfo> methodsList)
        {
            this.id = id;
            this.methodsList = methodsList;
        }



        private long SummMethodsExecutionTime(MethodInfo methodInfo)
        {
            long time = 0;
            time += methodInfo.getExecutionTime();
            foreach (MethodInfo method in methodInfo.getChildMethods())
            {
                if (method.getChildMethods().Count > 0)
                {
                    time += SummMethodsExecutionTime(method);
                }
                time += method.getExecutionTime();
            }
            return time;
        }

        public double ExecutionTime
        {
            get
            {
                if (methodsList.Count > 0)
                {
                    executionTime = SummMethodsExecutionTime(methodsList[0]); // check
                }
                return executionTime;
            }
            set
            {
                executionTime = value;
            }
        }
    }
}
