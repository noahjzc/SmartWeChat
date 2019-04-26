using Newtonsoft.Json;
using System.Net.Http;

namespace SmartWeChat.DTO.Menu
{
    public class DeleteConditionalMenuRequest : SWRequest<SWResponse>
    {
        [JsonProperty("menuid")]
        public long MenuId { get; set; }

        public override string GetApiUrl()
        {
            return "/cgi-bin/menu/delconditional";
        }

        public override HttpMethod Method()
        {
            return HttpMethod.Post;
        }
    }
}
