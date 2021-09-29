using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace TracerLib
{
    public class XMLSerialize : ISerialize
    {
        List<ThreadInfo> threadsInfo;
        XDocument xDocument = new XDocument(new XElement("threads"));
        public string serialize(TraceResult traceResult)
        {
            this.threadsInfo = traceResult.threadInfoList;

            foreach (ThreadInfo threadInfo in threadsInfo)
            {
                XElement threadXElement = getThread(threadInfo);
                xDocument.Root.Add(threadXElement);
            }

            StringWriter stringWriter = new StringWriter();
            using (XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter))
            {
                xmlWriter.Formatting = Formatting.Indented;
                xDocument.WriteTo(xmlWriter);
            }
            return stringWriter.ToString();
        }

        private XElement GetMethod(MethodInfo methodInfo)
        {
            return new XElement(
                "method",
                new XAttribute("name", methodInfo.name),
                new XAttribute("class", methodInfo.className),
                new XAttribute("time", methodInfo.executionTime)
                );
        }

        private XElement GetAllMethods(MethodInfo methodInfo)
        {
            XElement methodXElement = GetMethod(methodInfo);
            foreach (MethodInfo method in methodInfo.childMethods)
            {
                XElement childMethod = GetMethod(method);
                if (method.childMethods.Count > 0)
                {
                    childMethod = GetAllMethods(method);
                }
                methodXElement.Add(childMethod);
            }
            return methodXElement;
        }

        private XElement getThread(ThreadInfo threadInfo)
        {
            XElement threadXElement = new XElement(
                "thread",
                new XAttribute("id", threadInfo.id),
                new XAttribute("time", threadInfo.ExecutionTime)
                );
            foreach (MethodInfo method in threadInfo.methodsList)
            {
                XElement methodXElement = GetAllMethods(method);
                threadXElement.Add(methodXElement);
            }
            return threadXElement;
        }
    }
}
