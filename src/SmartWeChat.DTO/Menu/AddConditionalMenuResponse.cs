using Newtonsoft.Json;

namespace SmartWeChat.DTO.Menu
{
    public class AddConditionalMenuResponse : SWResponse
    {
        [JsonProperty("menuid")]
        public long MenuId { get; set; }
    }
}
