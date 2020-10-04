using System;
using RestSharp;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using RestSharpAPIAutomationDemo.RestSharpBase;
using System.Collections.Generic;

namespace RestSharpAPIAutomationDemo.Test
{
    [TestFixture]
    class UserDetails : BaseTest
    {
        private IRestResponse response;

        [Test]
        [Description("Verify that the Get User Details API returns userId, name and email")]
        [Author("Osanda Nimalarathna")]
        [Category("Get User Details")]
        public void VerifyGetUserDetailsApi()
        {
            Dictionary<String, String> pathParamMap = new Dictionary<String, String>();
            pathParamMap.Add("pathParam1", "users");
            pathParamMap.Add("pathParam2", "1");

            response = RestMethods.GetWithPathParam(pathParamMap, baseUrl);
            var responseJsonObject = JObject.Parse(response.Content);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseJsonObject.GetValue("userId"));
            Assert.NotNull(responseJsonObject.GetValue("name"));
            Assert.NotNull(responseJsonObject.GetValue("email"));
        }
    }
}