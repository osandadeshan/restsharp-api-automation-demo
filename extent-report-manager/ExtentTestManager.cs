using System;
using AventStack.ExtentReports;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RestSharpAPIAutomationDemo.ExtentReportManager
{
    class ExtentTestManager
    {
        public static ThreadLocal<ExtentTest> extentTest = new ThreadLocal<ExtentTest>();
        public static ExtentReports extent = ExtentManager.getExtent();
        public static ExtentTest test;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest getTest()
        {
            return extentTest.Value;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest createTest(String name, String description, String deviceId)
        {
            test = extent.CreateTest(name, name).AssignCategory(description);
            extentTest.Value = test;
            return getTest();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest createTest(String name, String description)
        {
            return createTest(name, description, Thread.CurrentThread.Name);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest createTest(String name)
        {
            return createTest(name, "Sample Test");
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void logger(String message)
        {
            getTest().Log(Status.Info, message + "<br>");
        }
    }
}