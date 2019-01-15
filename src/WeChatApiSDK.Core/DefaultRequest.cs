using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeChatApiSDK.Core.Configuration;
using WeChatApiSDK.Core.Utility;

namespace WeChatApiSDK.Core
{
    public class DefaultRequest : IRequest
    {
        private readonly string _host;
        private readonly ILogger<DefaultRequest> _logger;

        public DefaultRequest(ILogger<DefaultRequest> logger
            , WeChatConfig config)
        {
            _host = config.Host;
            _logger = logger;
        }

        public async Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(string url, TRequest reqMsg)
        {
            var client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri(_host);
                StringContent content = new StringContent(JsonConvert.SerializeObject(reqMsg), Encoding.UTF8);
                var resp = await client.PostAsync(url, content);
                string responseString = await resp.Content.ReadAsStringAsync();

                if (resp.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Response Status Code Not Ok, Response String={responseString}");
                }

                return JsonConvert.DeserializeObject<TResponse>(responseString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"DefaultRequest:PostAsync:{url} Error");
                throw new WeChatException(ex);
            }
            finally
            {
                client.Dispose();
            }
        }

        public async Task<TResponse> GetAsJsonAsync<TResponse>(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_host);
                    string resp = await client.GetStringAsync(url);
                    return JsonConvert.DeserializeObject<TResponse>(resp);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"DefaultRequest:GetAsync:{url} Error");
                throw new WeChatException(ex);
            }
        }
    }
}
