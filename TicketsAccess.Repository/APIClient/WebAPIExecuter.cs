using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TicketsAccess.Repository.APIClient
{
    public class WebAPIExecuter
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public WebAPIExecuter(string baseUrl, HttpClient httpClient)
        {
            _baseUrl = baseUrl;
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> InvokeGet<T>(string uri)
        {
            return await _httpClient.GetFromJsonAsync<T>(GetUrl(uri));
        }

        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            var response = await _httpClient.PostAsJsonAsync(GetUrl(uri), obj);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string uri, T obj)
        {
            var response = await _httpClient.PutAsJsonAsync(GetUrl(uri), obj);

            response.EnsureSuccessStatusCode();
        }

        public async Task InvokeDelete<T>(string uri)
        {
            var response = await _httpClient.DeleteAsync(GetUrl(uri));

            response.EnsureSuccessStatusCode();
        }

        private string GetUrl(string uri)
        {
            return $"{_baseUrl}/{uri}";
        }
    }
}
