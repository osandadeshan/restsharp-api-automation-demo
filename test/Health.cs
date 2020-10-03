using RestSharp;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using RestSharpDemo.ExtentReportManager;

namespace RestSharpDemo.TestSuite
{
    [TestFixture]
    class Health : ReportListener
    {
        [Test]
        [Category("Health")]
        public void checkHealthStatus()
        {
            var client = new RestClient("https://maxsoft-mock-server-demo.web.app");
            var request = new RestRequest("/say/hello?name=Osanda", DataFormat.Json);

            var response = client.Get(request);
            var responseJsonObject = JObject.Parse(response.Content);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Hello Osanda! Welcome to mock server.", responseJsonObject.GetValue("message").ToString());
        }
    }
}