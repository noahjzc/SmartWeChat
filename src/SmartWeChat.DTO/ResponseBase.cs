using Newtonsoft.Json;

namespace SmartWeChat.DTO
{
    public class ResponseBase
    {
        [JsonProperty("errcode")]
        public string ErrorCode { get; set; }

        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }
    }
}
