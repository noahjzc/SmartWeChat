using Newtonsoft.Json;

namespace WeChatApiSDK.Core.DTO.AccessToken
{
    public class AccessTokenResponse : ResponseBase
    {
        /// <summary>
        /// Token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
