using System.Net.Http;

namespace SmartWeChat.DTO.AccessToken
{
    public class AccessTokenRequest : SWRequest<AccessTokenResponse>
    {
        [QueryStringAlias(Name = "grant_type")]
        public string GrantType { get; set; }

        [QueryStringAlias(Name = "appid")]
        public string AppId { get; set; }

        [QueryStringAlias(Name = "secret")]
        public string Secret { get; set; }

        public override string GetApiUrl()
        {
            return "/cgi-bin/token";
        }

        public override HttpMethod Method()
        {
            return HttpMethod.Get;
        }
    }
}
