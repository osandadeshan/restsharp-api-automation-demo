using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.IO;

namespace RestSharpDemo.ExtentReportManager
{
    public class ExtentManager
    {
        private static ExtentReports extent;
        private static String filePath = System.Reflection.Assembly.GetExecutingAssembly().Location + "ExtentReports.html";
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
                    if (ConfigurationManager.AppSettings["REPORTSERVER"].Equals("true"))
                    {
                        extent.AttachReporter(klovReporter());
                    }

                    extent.AnalysisStrategy = AnalysisStrategy.Test;
                    extent.AddSystemInfo("Project", "RestSharp API Automation Demo");
                    extent.AddSystemInfo("Developer", "Osanda Nimalarathna");
                    extent.AddSystemInfo("Restsharp Version", "106.11.7");
                    extent.AddSystemInfo("Environment", "QA");
                    extent.AddSystemInfo("Operating System", "Windows 10 (64-bit)");
                    extent.AddSystemInfo("Eexecution Type", "Nunit");
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
            Directory.CreateDirectory(projectPath.ToString() + "report");
            String reportPath = projectPath + "report\\ExtentReport.html";
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);

            setReportName(htmlReporter);
            setReportTheme(htmlReporter);
            return htmlReporter;
        }

        private static ExtentKlovReporter klovReporter()
        {
            ExtentKlovReporter klov = new ExtentKlovReporter();
            if (isMongoPortHostProvided())
            {
                klov.InitMongoDbConnection(getMongoHost(), getMongoPort());
                String klovProjectName = ConfigurationManager.AppSettings["PROJECTNAME"];
                String klovReportName = ConfigurationManager.AppSettings["REPORTNAME"];
                String projectname = klovProjectName;
                String reportname = klovReportName;
                if (klovProjectName == null || klovReportName == null)
                {
                    projectname = setProjectName();
                    reportname = getReportName();
                }
                klov.ProjectName = projectname;
                klov.ReportName = reportname;
                klov.InitKlovServerConnection(ConfigurationManager.AppSettings["REPORT_URL"]);
            }
            return klov;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void setSystemInfoInReport(String parameter, String value)
        {
            if (extent == null)
            {
                getExtent();
            }
            extent.AddSystemInfo(parameter, value);
        }

        private static Boolean isMongoPortHostProvided()
        {
            if (ConfigurationManager.AppSettings["MONGODB_SERVER"] != null
                    && ConfigurationManager.AppSettings["MONGODB_PORT"] != null)
            {
                setMongoHost(ConfigurationManager.AppSettings["MONGODB_SERVER"]);
                setMongoPort(int.Parse(ConfigurationManager.AppSettings["MONGODB_PORT"]));
                return true;
            }
            else
            {
                setMongoHost("localhost");
                setMongoPort(27017);
                return true;
            }

        }

        private static void setMongoPort(object p)
        {
            throw new NotImplementedException();
        }

        private static String getMongoHost()
        {
            return mongoHost;
        }

        private static void setMongoHost(String mongo)
        {
            mongoHost = mongo;
        }

        private static int getMongoPort()
        {
            return mongoPort;
        }

        private static void setMongoPort(int port)
        {
            mongoPort = port;
        }
        private static void setReportTheme(ExtentHtmlReporter htmlReporter)
        {
            if (ConfigurationManager.AppSettings["THEME"] != null)
            {
                if (ConfigurationManager.AppSettings["THEME"].Equals("Standard"))
                {
                    htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
                }
                else
                {
                    htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
                }
            }
            else
            {
                htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            }
        }
        private static void setReportName(ExtentHtmlReporter htmlReporter)
        {
            if (ConfigurationManager.AppSettings["REPORTNAME"] != null)
            {
                htmlReporter.Config.DocumentTitle = ConfigurationManager.AppSettings["REPORTNAME"];
                htmlReporter.Config.ReportName = ConfigurationManager.AppSettings["REPORTNAME"];
            }
            else
            {
                htmlReporter.Config.DocumentTitle = "RestSharp API Automation Demo";
                htmlReporter.Config.ReportName = "RestSharp API Automation Demo";
            }
        }
        private static String getReportName()
        {
            String repName = "RestSharp API Automation Demo";
            if (ConfigurationManager.AppSettings["REPORTNAME"] != null)
            {
                repName = ConfigurationManager.AppSettings["REPORTNAME"];
            }
            return repName;
        }

        private static String setProjectName()
        {
            String projName = "RestSharp API Automation Demo";
            if (ConfigurationManager.AppSettings["PROJECTNAME"] != null)
            {
                projName = ConfigurationManager.AppSettings["PROJECTNAME"];
            }
            return projName;
        }
    }
}

