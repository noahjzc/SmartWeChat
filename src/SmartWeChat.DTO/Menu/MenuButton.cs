using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartWeChat.DTO.Menu
{
    public class RootButtonItem : ButtonItem
    {
        /// <summary>
        /// 二级菜单数组，个数应为1~5个
        /// </summary>
        [JsonProperty("sub_button")]
        public IEnumerable<ButtonItem> SubButton { get; set; }
    }

    public class ButtonItem
    {
        /// <summary>
        /// 菜单的响应动作类型，view表示网页类型，click表示点击类型，miniprogram表示小程序类型
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过60个字节
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// click等点击类型必须
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// view、miniprogram类型必须
        /// 网页 链接，用户点击菜单可打开链接，不超过1024字节。 type为miniprogram时，不支持小程序的老版本客户端将打开本url。
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// media_id类型和view_limited类型必须
        /// 调用新增永久素材接口返回的合法media_id
        /// </summary>
        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        /// <summary>
        /// miniprogram类型必须
        /// 小程序的appid（仅认证公众号可配置）
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// miniprogram类型必须
        /// 小程序的页面路径
        /// </summary>
        [JsonProperty("pagepath")]
        public string PagePath { get; set; }
    }
}
