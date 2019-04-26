using Microsoft.Extensions.Logging;
using SmartWeChat.DTO;
using SmartWeChat.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartWeChat
{
    public class DefaultRequest
    {
        private readonly ILogger<DefaultRequest> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultRequest(ILogger<DefaultRequest> logger, IHttpClientFactory sHttpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = sHttpClientFactory;
        }

        public async Task<TResponse> Execute<TResponse>(SWRequest<TResponse> request, string token = "") where TResponse : SWResponse
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                try
                {
                    string url = request.GetApiUrl() + "?";
                    if (!string.IsNullOrEmpty(token))
                    {
                        url += $"access_token={token}&";
                    }

                    string responseJson;
                    if (request.Method() == HttpMethod.Get)
                    {
                        url += GenerateQueryString(request);
                        //string url = $"{request.GetApiUrl()}?{GenerateQueryString(request)}";
                        responseJson = await client.GetStringAsync(url);

                    }
                    else
                    {
                        var content = new StringContent(request.ToJson(), Encoding.UTF8);
                        var resp = await client.PostAsync(url, content);
                        responseJson = await resp.Content.ReadAsStringAsync();
                    }

                    return responseJson.JsonDeserialize<TResponse>();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"DefaultRequest:Execute:{request.GetApiUrl()} Error");
                    throw new SmartWeChatException(ex);
                }
            }
        }

        private string GenerateQueryString<TResponse>(SWRequest<TResponse> request) where TResponse : SWResponse
        {
            var parameters = new List<string>();
            var properties = request.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(request);
                if (value != null)
                {
                    string queryName = property.Name;
                    if (property.GetCustomAttributes().FirstOrDefault(a => a.GetType() == typeof(QueryStringAliasAttribute)) is QueryStringAliasAttribute aliasAttribute)
                    {
                        queryName = aliasAttribute.Name;
                    }

                    parameters.Add($"{queryName}={value}");
                }
            }

            return string.Join("&", parameters);
        }


        //public async Task<TResponse> PostAsJsonAsync(SWRequest<TResponse> reqMsg) where TResponse : SWResponse
        //{
        //    using (var client = _httpClientFactory.CreateClient("SmartWeChat"))
        //    {
        //        try
        //        {
        //            var content = new StringContent(reqMsg.ToJson(), Encoding.UTF8);
        //            var url = reqMsg.GetApiUrl()+"?"

        //            var resp = await client.PostAsync(, content);
        //            string responseString = await resp.Content.ReadAsStringAsync();

        //            if (resp.StatusCode != HttpStatusCode.OK)
        //            {
        //                throw new Exception($"Response Status Code Not Ok, Response String={responseString}");
        //            }

        //            return responseString.JsonDeserialize<TResponse>();
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, $"DefaultRequest:PostAsync:{url} Error");
        //            throw new SmartWeChatException(ex);
        //        }
        //    }
        //}

        //public async Task<TResponse> GetAsJsonAsync<TResponse>(string url) where TResponse : SWResponse
        //{
        //    using (var client = _httpClientFactory.CreateClient("SmartWeChat"))
        //    {
        //        try
        //        {
        //            string resp = await client.GetStringAsync(url);
        //            return resp.JsonDeserialize<TResponse>();
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, $"DefaultRequest:GetAsync:{url} Error");
        //            throw new SmartWeChatException(ex);
        //        }
        //    }
        //}
    }
}
