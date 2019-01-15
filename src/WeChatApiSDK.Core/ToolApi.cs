using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WeChatApiSDK.Core.Configuration;
using WeChatApiSDK.Core.DTO.Tools;

namespace WeChatApiSDK.Core
{
    public class ToolApi : IPlugin
    {
        private readonly ILogger<ToolApi> _logger;
        private readonly DefaultRequest _request;
        private readonly WeChatConfig _config;
        private readonly AccessTokenManager _accessTokenManager;

        public ToolApi(ILogger<ToolApi> sLogger, DefaultRequest sRequest, WeChatConfig sConfig, AccessTokenManager sAccessTokenManager)
        {
            _logger = sLogger;
            _request = sRequest;
            _config = sConfig;
            _accessTokenManager = sAccessTokenManager;
        }

        public async Task<GetWeChatServerIPResponse> GetWeChatServerIP()
        {
            return await _request.GetAsJsonAsync<GetWeChatServerIPResponse>($"/cgi-bin/getcallbackip?access_token={await _accessTokenManager.GetTokenAsync()}");
        }
    }
}