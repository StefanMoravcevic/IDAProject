using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.AspNetCore.Http;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Managers
{
    public abstract class BaseManager
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly IConfiguration _configuration;
        protected readonly ILogger _logger;
        private const int RequestTimeout = 30;

        public BaseManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        protected async Task<T> GetAsync<T>(string relativePath)
        {
            var result = (T)Activator.CreateInstance(typeof(T))!;

            var client = GetHttpClient();

            var endpointPath = GetEndpointUrl(relativePath);
            var response = await client.GetAsync(endpointPath);
            if (response.IsSuccessStatusCode)
            {
                // https://stackoverflow.com/questions/10399324/where-is-httpcontent-readasasync
                result = await response.Content.ReadAsAsync<T>();
            }

            return result;
        }

        protected async Task<Out> SendAsync<Out>(string relativePath, HttpMethod method) where Out : ResponseModelBase
        {
            var result = (Out)Activator.CreateInstance(typeof(Out))!;

            var client = GetHttpClient();
            var uri = GetEndpointUri(relativePath);
            var requestMessage = new HttpRequestMessage(method, uri);

            var response = await client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                // https://stackoverflow.com/questions/10399324/where-is-httpcontent-readasasync
                result = await response.Content.ReadAsAsync<Out>();
            }
            else
            {
                _logger.LogError($"SendAsync [{method}] status code: {response.StatusCode}");
                if (response.Content != null)
                {
                    _logger.LogError(await response.Content.ReadAsStringAsync());
                }
                result.Message = "Error";
            }
            return result;
        }

        protected async Task<Out> SendAsync<In, Out>(string relativePath, In value, HttpMethod method) where Out : ResponseModelBase
        {
            var result = (Out)Activator.CreateInstance(typeof(Out))!;

            var client = GetHttpClient();

            var uri = GetEndpointUri(relativePath);
            var requestMessage = new HttpRequestMessage(method, uri);

            var jsonData = JsonConvert.SerializeObject(value);
            requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");


            var response = await client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                // https://stackoverflow.com/questions/10399324/where-is-httpcontent-readasasync
                result = await response.Content.ReadAsAsync<Out>();
            }
            else
            {
                _logger.LogError($"SendAsync [{method}] status code: {response.StatusCode}");
                if (response.Content != null)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    _logger.LogError(responseString);
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    result.Message = "Wrong username or password!";
                else
                    result.Message = "Error";
            }
            return result;
        }


        protected async Task<Out> PostAsync<In, Out>(string relativePath, In value) where Out : ResponseModelBase
        {
            var result = await SendAsync<In, Out>(relativePath, value, HttpMethod.Post);
            return result;
        }

        protected async Task<Out> PutAsync<In, Out>(string relativePath, In value) where Out : ResponseModelBase
        {
            var result = await SendAsync<In, Out>(relativePath, value, HttpMethod.Put);
            return result;
        }

        protected async Task<T> PutAsync<T>(string relativePath) where T : ResponseModelBase
        {
            var result = (T)Activator.CreateInstance(typeof(T))!;
            var client = GetHttpClient();

            var endpointPath = GetEndpointUrl(relativePath);
            var response = await client.PutAsync(endpointPath, null);
            if (response.IsSuccessStatusCode)
            {
                // https://stackoverflow.com/questions/10399324/where-is-httpcontent-readasasync
                result = await response.Content.ReadAsAsync<T>();
            }

            return result;
        }


        public async Task<ResponseModelBase> PostFile(byte[] dataBuffer, string relativePath)
        {
            var result = new ResponseModelBase();

            var client = GetHttpClient();

            var uri = GetEndpointUri(relativePath);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            requestMessage.Content = new ByteArrayContent(dataBuffer, 0, dataBuffer.Length);


            var response = await client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                // https://stackoverflow.com/questions/10399324/where-is-httpcontent-readasasync
                result = await response.Content.ReadAsAsync<ResponseModelBase>();
            }
            else
            {
                _logger.LogError($"PostFile status code: {response.StatusCode}");
                if (response.Content != null)
                {
                    _logger.LogError(await response.Content.ReadAsStringAsync());
                }
                result.Message = "Error";
            }
            return result;
        }

        protected async Task<T> DeleteAsync<T>(string relativePath)
        {
            var result = (T)Activator.CreateInstance(typeof(T))!;

            var client = GetHttpClient();

            var endpointPath = GetEndpointUrl(relativePath);
            var response = await client.DeleteAsync(endpointPath);

            if (response.IsSuccessStatusCode)
            {
                // https://stackoverflow.com/questions/10399324/where-is-httpcontent-readasasync
                result = await response.Content.ReadAsAsync<T>();
            }
            else
            {
                _logger.LogError($"DeleteAsync with status code: {response.StatusCode}");
                if (response.Content != null)
                {
                    _logger.LogError(await response.Content.ReadAsStringAsync());
                }
            }

            return result;
        }

        protected HttpClient GetHttpClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            client.Timeout = new TimeSpan(0, RequestTimeout, 0);
            UpdateRequestHeaders(client.DefaultRequestHeaders);
            return client;
        }

        public string GetEndpointUrl(string relativePath)
        {
            var webApiUrl = _configuration["WebApi:Url"]!;

            if (webApiUrl.EndsWith("/") && relativePath.StartsWith("/"))
            {
                webApiUrl = webApiUrl.Substring(0, webApiUrl.Length - 1);
            }
            else if (!webApiUrl.EndsWith("/") && !relativePath.StartsWith("/"))
            {
                relativePath = $"/{relativePath}";
            }

            var url = webApiUrl + relativePath;
            return url;
        }

        #region Private methods


        private Uri GetEndpointUri(string relativePath)
        {
            var url = GetEndpointUrl(relativePath);
            var uri = new Uri(url, UriKind.Absolute);
            return uri;
        }

        private void UpdateRequestHeaders(HttpRequestHeaders requestHeraders)
        {
            var webApiKey = _configuration["WebApi:Key"];
            requestHeraders.Add(Constants.ApiKeyName, webApiKey);
            //var token = _httpContextAccessor.HttpContext.Request.Cookies[Constants.AdminCookieToken];
            //requestHeraders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
        }

        #endregion
    }
}
