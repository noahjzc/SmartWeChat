using Newtonsoft.Json;
using System.Net.Http;

namespace SmartWeChat.DTO.Menu
{
    public class TryMatchMenuRequest : SWRequest<TryMatchMenuResponse>
    {
        /// <summary>
        /// user_id可以是粉丝的OpenID，也可以是粉丝的微信号。
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        public override string GetApiUrl()
        {
            return "/cgi-bin/menu/trymatch";
        }

        public override HttpMethod Method()
        {
            return HttpMethod.Post;
        }
    }
}
