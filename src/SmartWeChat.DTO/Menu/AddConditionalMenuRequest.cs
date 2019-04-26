using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace SmartWeChat.DTO.Menu
{
    public class AddConditionalMenuRequest : SWRequest<AddConditionalMenuResponse>
    {
        [JsonProperty("button")]
        public IEnumerable<RootButtonItem> Button { get; set; }

        [JsonProperty("matchrule")]
        public MatchRuleModel MatchRule { get; set; }

        public class MatchRuleModel
        {
            [JsonProperty("tag_id")]
            public string TagId { get; set; }

            /// <summary>
            /// 性别：男（1）女（2），不填则不做匹配
            /// </summary>
            [JsonProperty("sex")]
            public string Sex { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("province")]
            public string Province { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            /// <summary>
            /// 客户端版本，当前只具体到系统型号：IOS(1), Android(2),Others(3)，不填则不做匹配
            /// </summary>
            [JsonProperty("client_platform_type")]
            public string ClientPlatformType { get; set; }

            /// <summary>
            /// 语言信息，是用户在微信中设置的语言，具体请参考语言表： 1、简体中文 "zh_CN" 2、繁体中文TW "zh_TW" 3、繁体中文HK "zh_HK" 4、英文 "en" 5、印尼 "id" 6、马来 "ms" 7、西班牙 "es" 8、韩国 "ko" 9、意大利 "it" 10、日本 "ja" 11、波兰 "pl" 12、葡萄牙 "pt" 13、俄国 "ru" 14、泰文 "th" 15、越南 "vi" 16、阿拉伯语 "ar" 17、北印度 "hi" 18、希伯来 "he" 19、土耳其 "tr" 20、德语 "de" 21、法语 "fr"
            /// </summary>
            [JsonProperty("language")]
            public string Language { get; set; }
        }

        public override string GetApiUrl()
        {
            return "/cgi-bin/menu/addconditional";
        }

        public override HttpMethod Method()
        {
            return HttpMethod.Post;
        }
    }
}
