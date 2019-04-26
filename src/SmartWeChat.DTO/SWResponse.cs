using Newtonsoft.Json;

namespace SmartWeChat.DTO
{
    public class SWResponse
    {
        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }
    }
}
