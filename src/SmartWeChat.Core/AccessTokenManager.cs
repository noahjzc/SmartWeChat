using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SmartWeChat.Configuration;
using SmartWeChat.DTO.AccessToken;
using System;
using System.Threading.Tasks;

namespace SmartWeChat.Core
{
    public class AccessTokenManager
    {
        private readonly ILogger<AccessTokenManager> _logger;
        private readonly SmartWeChatOptions _config;
        private readonly IMemoryCache _memoryCache;
        private readonly DefaultRequest _request;

        public AccessTokenManager(ILogger<AccessTokenManager> logger
            , SmartWeChatOptions config
            , IMemoryCache sMemoryCache, DefaultRequest sRequest)
        {
            _logger = logger;
            _config = config;
            _memoryCache = sMemoryCache;
            _request = sRequest;
        }

        /// <summary>
        /// 异步获取Token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTokenAsync()
        {
            return await _memoryCache.GetOrCreateAsync<string>($"SmartWeChat_AccessToken_{_config.AppId}", async (cacheEntry) =>
               {
                   var tokenResponse = await _request.GetAsJsonAsync<AccessTokenResponse>(
                          $"/cgi-bin/token?grant_type=client_credential&appid={_config.AppId}&secret={_config.AppSecret}");

                   _logger.LogDebug($"AccessTokenManager Get Token--{tokenResponse.AccessToken}");
                   // 放宽5分钟的缓存时间，防止延迟导致Token失效的访问
                   cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(tokenResponse.ExpiresIn - (60 * 5));
                   return tokenResponse.AccessToken;
               });
        }
    }
}
