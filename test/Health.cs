using System;
using RestSharp;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using RestSharpAPIAutomationDemo.RestSharpBase;
using System.Collections.Generic;

namespace RestSharpAPIAutomationDemo.Test
{
    [TestFixture]
    class Health : BaseTest
    {
        private IRestResponse response;

        [Test]
        [Description("Verify that the Health route is up and running")]
        [Author("Osanda Nimalarathna")]
        [Category("Health")]
        public void VerifyHealthStatus()
        {
            Dictionary<String, String> pathParamMap = new Dictionary<String, String>();
            pathParamMap.Add("pathParam1", "say");
            pathParamMap.Add("pathParam2", "hello");

            Dictionary<String, String> queryParamMap = new Dictionary<String, String>();
            queryParamMap.Add("name", "Osanda");

            response = RestMethods.GetWithPathAndQueryParam(pathParamMap, queryParamMap, baseUrl);
            var responseJsonObject = JObject.Parse(response.Content);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Hello Osanda! Welcome to mock server.", responseJsonObject.GetValue("message").ToString());
        }
    }
}