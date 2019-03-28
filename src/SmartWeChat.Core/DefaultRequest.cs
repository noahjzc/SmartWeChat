using Microsoft.Extensions.Logging;
using SmartWeChat.Core.Abstractions;
using SmartWeChat.DTO;
using SmartWeChat.Utility;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartWeChat.Core
{
    public class DefaultRequest : IRequest
    {
        private readonly ILogger<DefaultRequest> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultRequest(ILogger<DefaultRequest> logger, IHttpClientFactory sHttpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = sHttpClientFactory;
        }

        public async Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(string url, TRequest reqMsg) where TResponse : ResponseBase where TRequest : IRequestModel
        {
            using (var client = _httpClientFactory.CreateClient("SmartWeChat"))
            {
                try
                {
                    var content = new StringContent(reqMsg.JsonSerialize(), Encoding.UTF8);
                    var resp = await client.PostAsync(url, content);
                    string responseString = await resp.Content.ReadAsStringAsync();

                    if (resp.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception($"Response Status Code Not Ok, Response String={responseString}");
                    }

                    return responseString.JsonDeserialize<TResponse>();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"DefaultRequest:PostAsync:{url} Error");
                    throw new SmartWeChatException(ex);
                }
            }
        }

        public async Task<TResponse> GetAsJsonAsync<TResponse>(string url) where TResponse : ResponseBase
        {
            using (var client = _httpClientFactory.CreateClient("SmartWeChat"))
            {
                try
                {
                    string resp = await client.GetStringAsync(url);
                    return resp.JsonDeserialize<TResponse>();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"DefaultRequest:GetAsync:{url} Error");
                    throw new SmartWeChatException(ex);
                }
            }
        }
    }
}
