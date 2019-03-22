using Newtonsoft.Json;

namespace SmartWeChat.DTO.Tools
{
    public class NetCheckRequest : IRequestModel
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("check_operator")]
        public string CheckOperator { get; set; }

        public class NetCheckAction
        {
            /// <summary>
            /// 做域名解析
            /// </summary>
            public const string DNS = "dns";

            /// <summary>
            /// 做ping检测
            /// </summary>
            public const string PING = "ping";

            /// <summary>
            /// 都做
            /// </summary>
            public const string ALL = "all";
        }

        public class NetCheck_CheckOperator
        {
            /// <summary>
            /// 电信出口
            /// </summary>
            public const string CHINANET = "CHINANET";

            /// <summary>
            /// 联通出口
            /// </summary>
            public const string UNICOM = "UNICOM";

            /// <summary>
            /// 腾讯自建出口
            /// </summary>
            public const string CAP = "CAP";

            /// <summary>
            /// 根据ip来选择运营商
            /// </summary>
            public const string DEFAULT = "DEFAULT";
        }

        public override bool Validate()
        {
            return true;
        }
    }
}