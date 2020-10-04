using System;
using System.Collections.Generic;
using RestSharp;

namespace RestSharpAPIAutomationDemo.RestSharpBase
{
    class RestMethods
    {
        public static IRestResponse response;
        public static IRestRequest request;
        public static String requestUrl;

        public static IRestResponse Get(String url)
        {
            requestUrl = url;
            request = new RestRequest(url, DataFormat.Json);
            response = new RestClient().Get(request);
            return response;
        }

        public static IRestResponse GetWithPathParam(Dictionary<String, String> pathParamMap, String url)
        {
            requestUrl = url;
            String pathParam = "";
            foreach (var param in pathParamMap)
            {
                String key = "/";
                String value = param.Value;
                pathParam = pathParam + key + value;
            }
            request = new RestRequest(url + pathParam, DataFormat.Json);
            response = new RestClient().Get(request);
            return response;
        }

        public static IRestResponse GetWithQueryParam(Dictionary<String, String> queryParamMap, String url)
        {
            requestUrl = url;
            String queryParam = "";
            foreach (var param in queryParamMap)
            {
                String key = param.Key;
                String value = param.Value;
                queryParam = queryParam + "?" + key + "=" + value;
            }
            request = new RestRequest(url + queryParam, DataFormat.Json);
            response = new RestClient().Get(request);
            return response;
        }

        public static IRestResponse GetWithPathAndQueryParam(Dictionary<String, String> pathParamMap, Dictionary<String, String> queryParamMap, String url)
        {
            requestUrl = url;
            String pathParam = "";
            foreach (var param in pathParamMap)
            {
                String key = "/";
                String value = param.Value;
                pathParam = pathParam + key + value;
            }
            String queryParam = "";
            foreach (var param in queryParamMap)
            {
                String key = param.Key;
                String value = param.Value;
                queryParam = queryParam + "?" + key + "=" + value;
            }
            request = new RestRequest(url + pathParam + queryParam, DataFormat.Json);
            response = new RestClient().Get(request);
            return response;
        }

        public static IRestResponse PostWithJsonBodyParam(String jsonObject, String url)
        {
            requestUrl = url;
            request = new RestRequest(url, DataFormat.Json).AddJsonBody(jsonObject);
            response = new RestClient().Post(request);
            return response;
        }

        public static IRestResponse PostWithJsonBodyParam(JsonObject jsonObj, String url)
        {
            requestUrl = url;
            request = new RestRequest(url, DataFormat.Json).AddJsonBody(jsonObj);
            response = new RestClient().Post(request);
            return response;
        }

        public static IRestResponse PutWithJsonBodyParam(JsonObject jsonObject, String url)
        {
            requestUrl = url;
            request = new RestRequest(url, DataFormat.Json).AddJsonBody(jsonObject);
            response = new RestClient().Put(request);
            return response;
        }

        public static IRestResponse DeleteWithPathParam(String url)
        {
            requestUrl = url;
            request = new RestRequest(url, DataFormat.Json);
            response = new RestClient().Delete(request);
            return response;
        }
    }
}