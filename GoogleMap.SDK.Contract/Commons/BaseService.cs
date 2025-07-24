using Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GoogleMap.SDK.API.Commons
{
    
    public class BaseService
    {
        private string apiKey;
        protected HttpRequest httpRequest;
        public BaseService(IConfiguration configuration)
        {
            apiKey = configuration.GetSection("apiKey").Value;
            httpRequest = new HttpRequest();
            httpRequest.BaseUrl = $"https://maps.googleapis.com/maps/api/";
            httpRequest.AddRequestHeader("X-Goog-Api-Key", apiKey);
            httpRequest.AddRequestHeader("X-Goog-FieldMask", "*");
        }

        public async Task<T> GetAsync<T>(string url)
        {
            url = $"{url}&key={apiKey}";   
            var response = await httpRequest.GetAsync<T>(url);
            return response;
        }
        public async Task<T> PostAsync<T>(string url, object data)
        {
            var response = await httpRequest.PostAsync<T>(url, data);
            return response;
        }

        public void AddHttpClientHeader(string key, string value)
        {
            httpRequest.AddRequestHeader(key, value);
        }
        public void AddHttpClientHeader(Dictionary<string, string> headers)
        {
            httpRequest.AddRequestHeaders(headers);
        }
    }
}
