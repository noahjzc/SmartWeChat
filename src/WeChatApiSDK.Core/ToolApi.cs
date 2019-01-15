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

        /// <summary>
        /// 获取微信服务器IP
        /// </summary>
        /// <returns></returns>
        public async Task<GetWeChatServerIPResponse> GetWeChatServerIP()
        {
            return await _request.GetAsJsonAsync<GetWeChatServerIPResponse>($"/cgi-bin/getcallbackip?access_token={await _accessTokenManager.GetTokenAsync()}");
        }

        /// <summary>
        /// 网络检测
        /// </summary>
        /// <param name="reqMsg"></param>
        /// <returns></returns>
        public async Task<NetCheckResponse> NetCheck(NetCheckRequest reqMsg)
        {
            return await _request.PostAsJsonAsync<NetCheckRequest, NetCheckResponse>($"/cgi-bin/callback/check?access_token={await _accessTokenManager.GetTokenAsync()}", reqMsg);
        }
    }
}