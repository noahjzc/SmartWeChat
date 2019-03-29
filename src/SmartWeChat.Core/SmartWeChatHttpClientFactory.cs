using SmartWeChat.Configuration;
using System;
using System.Net.Http;

namespace SmartWeChat
{
    public class SmartWeChatHttpClientFactory : IHttpClientFactory
    {
        public string Host { get; private set; }

        public SmartWeChatHttpClientFactory(SmartWeChatOptions options)
        {
            Host = options.Host;
        }

        public HttpClient CreateClient(string name)
        {
            return new HttpClient
            {
                BaseAddress = new Uri(Host)
            };
        }
    }
}
