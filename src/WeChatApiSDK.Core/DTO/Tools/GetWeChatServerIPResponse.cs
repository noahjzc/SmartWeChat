using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeChatApiSDK.Core.DTO.Tools
{
    public class GetWeChatServerIPResponse : ResponseBase
    {
        /// <summary>
        /// IP 地址列表
        /// </summary>
        [JsonProperty("ip_list")]
        public IEnumerable<string> IpList { get; set; }
    }
}
