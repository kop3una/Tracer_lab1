using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;


namespace TracerLib
{
    public class JSONSerialize : ISerialize
    {
        public string serialize(TraceResult traceResult)
        {
            JArray threadJArray = new JArray();
            foreach (ThreadInfo threadInfo in traceResult.threadInfoList)
            {
                JObject threadJObject = GetThread(threadInfo);
                threadJArray.Add(threadJObject);
            }
            JObject resultJObject = new JObject
            {
                {"threads", threadJArray }
            };
            StringWriter stringWriter = new StringWriter();
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;
                resultJObject.WriteTo(jsonWriter);
            }
            return stringWriter.ToString();
        }

        private JObject GetMethod(MethodInfo methodInfo)
        {
            return new JObject
            {
                {"name", methodInfo.name },
                {"class", methodInfo.className },
                {"time", methodInfo.executionTime},
            };
        }

        private JObject GetAllMethods(MethodInfo methodInfo)
        {
            JObject methodJObject = GetMethod(methodInfo);
            JArray methodsJArray = new JArray();
            foreach (MethodInfo method in methodInfo.childMethods)
            {
                JObject childMethodJObject = GetMethod(method);
                if (method.childMethods.Count > 0)
                {
                    childMethodJObject = GetAllMethods(method);
                }
                methodsJArray.Add(childMethodJObject);
            }
            methodJObject.Add("methods", methodsJArray);
            return methodJObject;
        }

        private JObject GetThread(ThreadInfo threadInfo)
        {
            JArray methodJArray = new JArray();
            foreach (MethodInfo methodInfo in threadInfo.methodsList)
            {
                JObject methodJObject = GetAllMethods(methodInfo);

                methodJArray.Add(methodJObject);
            }
            return new JObject
            {
                {"id", threadInfo.id },
                {"time", threadInfo.ExecutionTime },
                {"methods", methodJArray }
            };
        }
    }
}
