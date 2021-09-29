using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TracerLib
{
    public class MethodInfo {
       public string name;
        public string className;
        public long executionTime;
        public List<MethodInfo> childMethods;

       public string getName()
        {
            return name;
        } 

        public void setName(string name)
        {
          this.name = name;
       }

        public string getClassName()
        {
            return className;
        }

        public void setClassName(string className)
        {
            this.className = className;
        }

        public long getExecutionTime()
        {
            return executionTime;
        }

        public void setExecutionTime(long executionTime)
        {
            this.executionTime = executionTime;
        }

        public List<MethodInfo> getChildMethods()
        {
            return childMethods;
        }

        public void setChildMethods(List<MethodInfo> childMethods)
        {
            this.childMethods = childMethods;
        }

    }
}
