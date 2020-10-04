using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace RestSharpAPIAutomationDemo.util
{
    class PropertyReader
    {
        public static String GetPropertyValue(String propertyName)
        {
            String path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            String actualPath = path.Substring(0, path.LastIndexOf("bin"));
            String projectPath = new Uri(actualPath).LocalPath;
            String reportPath = projectPath + "\\App.properties";
            var data = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines(reportPath))
            data.Add(row.Split('=')[0], string.Join("=",row.Split('=').Skip(1).ToArray()));
            return data[propertyName];
        }
    }
}