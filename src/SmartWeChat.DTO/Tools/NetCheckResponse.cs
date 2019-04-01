using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartWeChat.DTO.Tools
{
    public class NetCheckResponse : SWResponse
    {
        [JsonProperty("dns")]
        public IEnumerable<DnsState> Dns { get; set; }

        [JsonProperty("ping")]
        public IEnumerable<PingState> Ping { get; set; }

        public class DnsState
        {
            /// <summary>
            /// 解析出来的ip
            /// </summary>
            [JsonProperty("ip")]
            public string IP { get; set; }

            /// <summary>
            /// ip对应的运营商
            /// </summary>
            [JsonProperty("real_operator")]
            public string RealOperator { get; set; }
        }

        public class PingState
        {
            /// <summary>
            /// ping的ip，执行命令为ping ip –c 1-w 1 -q
            /// </summary>
            [JsonProperty("ip")]
            public string IP { get; set; }

            /// <summary>
            /// ping的源头的运营商，由请求中的check_operator控制
            /// </summary>
            [JsonProperty("from_operator")]
            public string FromOperator { get; set; }

            /// <summary>
            /// ping的丢包率，0%表示无丢包，100%表示全部丢包。因为目前仅发送一个ping包，因此取值仅有0%或者100%两种可能。
            /// </summary>
            [JsonProperty("package_loss")]
            public string PackageLoss { get; set; }

            /// <summary>
            /// ping的耗时，取ping结果的avg耗时。
            /// </summary>
            [JsonProperty("time")]
            public string Time { get; set; }
        }

    }
}
