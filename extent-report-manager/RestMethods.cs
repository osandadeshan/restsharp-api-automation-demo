using System;
using System.Collections.Generic;
using RestSharp;

namespace RestSharpAPIAutomationDemo.ExtentReportManager
{
    class RestMethods
    {
        public static IRestResponse res;
        public static IRestRequest request;
        public static String reqURL;

        public static IRestResponse getCall(String URL2)
        {
            reqURL = URL2;
            request = new RestRequest(URL2, DataFormat.Json);
            res = new RestClient().Get(request);
            return res;
        }

        public static IRestResponse getWithPathParam(Dictionary<String, String> paramMap, String URL2)
        {
            reqURL = URL2;
            String pathparam = null;
            foreach (var entry in paramMap)
            {
                String key = entry.Key;
                String value = entry.Value;
                pathparam = key + value;
            }
            request = new RestRequest(URL2 + "{pageID}", DataFormat.Json).AddParameter("pageID", pathparam);
            res = new RestClient().Get(request);
            return res;
        }

        public static IRestResponse postWithJsonBodyParam(String jsonObject, String URL2, String contentType)
        {
            reqURL = URL2;
            request = new RestRequest(URL2, DataFormat.Json).AddJsonBody(jsonObject);
            res = new RestClient().Post(request);
            return res;
        }

        public static IRestResponse postWithJsonBodyParam(JsonObject jsonObj, String URL2)
        {
            reqURL = URL2;
            request = new RestRequest(URL2, DataFormat.Json).AddJsonBody(jsonObj);
            res = new RestClient().Post(request);
            return res;
        }

        public static IRestResponse putWithJsonBodyParam(JsonObject jsonObject, String URL2)
        {
            reqURL = URL2;
            request = new RestRequest(URL2, DataFormat.Json).AddJsonBody(jsonObject);
            res = new RestClient().Put(request);
            return res;
        }

        public static IRestResponse deleteWithPathParam(String URL2)
        {
            reqURL = URL2;
            request = new RestRequest(URL2, DataFormat.Json);
            res = new RestClient().Delete(request);
            return res;
        }
    }
}