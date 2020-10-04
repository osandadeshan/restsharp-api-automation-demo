using System;
using NUnit.Framework;
using RestSharpAPIAutomationDemo.ExtentReportManager;
using RestSharpAPIAutomationDemo.RestSharpBase;

namespace RestSharpAPIAutomationDemo.Test
{
    [TestFixture]
    class BaseTest : ReportListener
    {
        protected String baseUrl = Constants.BASE_URL;
    }
}