using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Linq;
using RestSharpAPIAutomationDemo.RestSharpBase;

namespace RestSharpAPIAutomationDemo.ExtentReportManager
{
    [SetUpFixture]
    public abstract class ReportListener
    {
        [OneTimeSetUp]
        protected void Setup()
        {
            ExtentReportManager.ExtentManager.GetExtent();
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            ExtentReportManager.ExtentManager.GetExtent().Flush();
        }

        [SetUp]
        public void BeforeTest()
        {
            String methodName = TestContext.CurrentContext.Test.MethodName;
            ExtentReportManager.ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name, GetTestCategories(methodName));
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            var errorMessage = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.Message);        
            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Fail, "<b style=\"color: Red; \">Failed</b>");
                    ExtentReportManager.ExtentTestManager.GetTest().Log(logstatus, "<b>Exception: </b> <br />" + errorMessage);
                    ExtentReportManager.ExtentTestManager.GetTest().Log(logstatus, "<b>StackTrace: </b> <br />" + stacktrace);
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Info, "<b>Response URL: </b> <br />" + RestMethods.response.ResponseUri.AbsoluteUri);
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Info, "<b>Response Status: </b> <br />" + RestMethods.response.ResponseStatus.ToString());
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Info, "<b>Response Status Code: </b> <br />" + RestMethods.response.StatusCode.ToString());
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Info, "<b>Response: </b> <br />" + RestMethods.response.Content);
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Warning, "<b style=\"color: Tomato; \">Warning</b>");
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Skip, "<b style=\"color: Orange; \">Skipped</b>");
                    break;
                default:
                    logstatus = Status.Pass;
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Pass, "<b style=\"color: MediumSeaGreen; \">Passed</b>");
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Info, "<b>Response URL: </b> <br />" + RestMethods.response.ResponseUri.AbsoluteUri);
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Info, "<b>Response Status: </b> <br />" + RestMethods.response.ResponseStatus.ToString());
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Info, "<b>Response Status Code: </b> <br />" + RestMethods.response.StatusCode.ToString());
                    ExtentReportManager.ExtentTestManager.GetTest().Log(Status.Info, "<b>Response: </b> <br />" + RestMethods.response.Content);
                    break;
            }
            ExtentReportManager.ExtentManager.GetExtent().Flush();
        }

        public String GetTestCategories(String methodName)
        {
            var myAttribute = this.GetType().GetMethod(methodName).GetCustomAttributes(true).OfType<CategoryAttribute>().FirstOrDefault();
            return myAttribute.Name;
        }
    }
}