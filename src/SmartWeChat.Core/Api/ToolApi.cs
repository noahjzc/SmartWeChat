using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SmartWeChat.DTO.Tools;

namespace SmartWeChat
{
    //public class ToolApi : IModule
    //{
    //    private readonly ILogger<ToolApi> _logger;
    //    private readonly DefaultRequest _request;
    //    private readonly TokenManager _accessTokenManager;

    //    public ToolApi(ILogger<ToolApi> sLogger, DefaultRequest sRequest, TokenManager sAccessTokenManager)
    //    {
    //        _logger = sLogger;
    //        _request = sRequest;
    //        _accessTokenManager = sAccessTokenManager;
    //    }

    //    /// <summary>
    //    /// 获取微信服务器IP
    //    /// </summary>
    //    /// <returns></returns>
    //    public async Task<GetWeChatServerIPResponse> GetWeChatServerIP()
    //    {
    //        string token = await _accessTokenManager.GetTokenAsync();
    //        _logger.LogDebug($"GetWeChatServerIP-Token:{token}");
    //        return await _request.GetAsJsonAsync<GetWeChatServerIPResponse>($"/cgi-bin/getcallbackip?access_token={token}");
    //    }

    //    /// <summary>
    //    /// 网络检测
    //    /// </summary>
    //    /// <param name="reqMsg"></param>
    //    /// <returns></returns>
    //    public async Task<NetCheckResponse> NetCheck(NetCheckRequest reqMsg)
    //    {
    //        string token = await _accessTokenManager.GetTokenAsync();
    //        _logger.LogDebug($"NetCheck-Token:{token}");
    //        return await _request.PostAsJsonAsync<NetCheckRequest, NetCheckResponse>($"/cgi-bin/callback/check?access_token={token}", reqMsg);
    //    }
    //}
}