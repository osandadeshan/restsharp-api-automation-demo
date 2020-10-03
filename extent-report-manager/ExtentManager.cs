using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Runtime.CompilerServices;
using System.IO;

namespace RestSharpAPIAutomationDemo.ExtentReportManager
{
    public class ExtentManager
    {
        private static ExtentReports extent;
        private static String projectName = "RestSharp API Automation Demo";
        private static String reportName = "RestSharp API Automation Demo Test Results";
        private static String developerName = "Osanda Nimalarathna";
        private static String restsharpVersion = "106.11.7";
        private static String environment = "QA";
        private static String operatingSystem = "Windows 10 (64-bit)";
        private static String executionType = "NUnit";
        private static Boolean reportServer = false;
        private static String reportUrl = "";
        private static String mongoHost;
        private static int mongoPort;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentReports getExtent()
        {
            if (extent == null)
            {
                try
                {
                    extent = new ExtentReports();
                    extent.AttachReporter(getHtmlReporter());
                    if (reportServer)
                    {
                        extent.AttachReporter(klovReporter());
                    }
                    extent.AnalysisStrategy = AnalysisStrategy.Test;
                    extent.AddSystemInfo("Project", projectName);
                    extent.AddSystemInfo("Developer", developerName);
                    extent.AddSystemInfo("Restsharp Version", restsharpVersion);
                    extent.AddSystemInfo("Environment", environment);
                    extent.AddSystemInfo("Operating System", operatingSystem);
                    extent.AddSystemInfo("Eexecution Type", executionType);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            return extent;
        }

        private static ExtentHtmlReporter getHtmlReporter()
        {
            String path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            String actualPath = path.Substring(0, path.LastIndexOf("bin"));
            String projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "html-report");
            String reportPath = projectPath + "html-report\\ExtentReport.html";
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);
            
            htmlReporter.Config.DocumentTitle = reportName;
            htmlReporter.Config.ReportName = reportName;
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            return htmlReporter;
        }

        private static ExtentKlovReporter klovReporter()
        {
            ExtentKlovReporter klov = new ExtentKlovReporter();
            klov.InitMongoDbConnection("localhost", 27017);
            klov.ProjectName = projectName;
            klov.ReportName = reportName;
            klov.InitKlovServerConnection(reportUrl);
            return klov;
        }
    }
}