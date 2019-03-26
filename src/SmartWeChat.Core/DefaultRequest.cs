using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SmartWeChat.Core.Abstractions;
using SmartWeChat.Configuration;
using SmartWeChat.DTO;
using SmartWeChat.Utility;
using SmartWeChat.Utility;

namespace SmartWeChat.Core
{
    public class DefaultRequest : IRequest
    {
        private readonly string _host;
        private readonly ILogger<DefaultRequest> _logger;

        public DefaultRequest(ILogger<DefaultRequest> logger
            , SmartWeChatOptions config)
        {
            _host = config.Host;
            _logger = logger;
        }

        public async Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(string url, TRequest reqMsg) where TResponse : ResponseBase where TRequest : IRequestModel
        {
            if (!reqMsg.Validate())
            {
                return (new ResponseBase
                {
                    ErrorCode = "-0001",
                    ErrorMessage = reqMsg.ErrorMessages
                }) as TResponse;
            }

            var client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri(_host);
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
            finally
            {
                client.Dispose();
            }
        }

        public async Task<TResponse> GetAsJsonAsync<TResponse>(string url) where TResponse : ResponseBase
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_host);
                    string resp = await client.GetStringAsync(url);
                    return resp.JsonDeserialize<TResponse>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"DefaultRequest:GetAsync:{url} Error");
                throw new SmartWeChatException(ex);
            }
        }
    }
}
