using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace SmartWeChat.DTO.Menu
{
    public class GetCurrentSelfmenuInfoResponse : SWResponse
    {
        [JsonIgnore]
        public bool IsMenuOpen => IsMenuOpenValue == 1;

        [JsonProperty("is_menu_open")]
        public int IsMenuOpenValue { get; set; }

        [JsonProperty("selfmenu_info")]
        public SelfMenuInfoModel SelfMenuInfo { get; set; }

        public class SelfMenuInfoModel
        {
            [JsonProperty("button")]
            public IEnumerable<SelfMenuInfoRootButton> Button { get; set; }

        }

        public class SelfMenuInfoRootButton : SelfMenuInfoButton
        {
            [JsonProperty("sub_button")]
            public SelfMenuSubButton SubButton { get; set; }
        }

        public class SelfMenuSubButton
        {
            [JsonProperty("list")]
            public IEnumerable<SelfMenuInfoButton> List { get; set; }
        }

        public class SelfMenuInfoButton
        {
            /// <summary>
            /// 菜单的类型，公众平台官网上能够设置的菜单类型有view（跳转网页）、text（返回文本，下同）、img、photo、video、voice。使用API设置的则有8种，详见《自定义菜单创建接口》
            /// </summary>
            [JsonProperty("type")]
            public string Type { get; set; }

            /// <summary>
            /// 菜单名称
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// 对于不同的菜单类型，value的值意义不同。官网上设置的自定义菜单：
            /// Text:保存文字到value； Img、voice：保存mediaID到value；
            /// Video：保存视频下载链接到value
            /// News：保存图文消息到news_info，同时保存mediaID到value；
            /// View：保存链接到url。 使用API设置的自定义菜单： click、scancode_push、scancode_waitmsg、pic_sysphoto、pic_photo_or_album、 pic_weixin、location_select：保存值到key；view：保存链接到url
            /// </summary>
            [JsonProperty("url")]
            public string Url { get; set; }

            /// <summary>
            /// 对于不同的菜单类型，value的值意义不同。官网上设置的自定义菜单：
            /// Text:保存文字到value； Img、voice：保存mediaID到value；
            /// Video：保存视频下载链接到value
            /// News：保存图文消息到news_info，同时保存mediaID到value；
            /// View：保存链接到url。 使用API设置的自定义菜单： click、scancode_push、scancode_waitmsg、pic_sysphoto、pic_photo_or_album、 pic_weixin、location_select：保存值到key；view：保存链接到url
            /// </summary>
            [JsonProperty("value")]
            public string Value { get; set; }

            /// <summary>
            /// 对于不同的菜单类型，value的值意义不同。官网上设置的自定义菜单：
            /// Text:保存文字到value； Img、voice：保存mediaID到value；
            /// Video：保存视频下载链接到value
            /// News：保存图文消息到news_info，同时保存mediaID到value；
            /// View：保存链接到url。 使用API设置的自定义菜单： click、scancode_push、scancode_waitmsg、pic_sysphoto、pic_photo_or_album、 pic_weixin、location_select：保存值到key；view：保存链接到url
            /// </summary>
            [JsonProperty("key")]
            public string Key { get; set; }

            [JsonProperty("news_info")]
            public IEnumerable<NewsInfoModel> NewsInfo { get; set; }
        }

        /// <summary>
        /// 图文消息的信息
        /// </summary>
        public class NewsInfoModel
        {
            /// <summary>
            /// 图文消息的标题
            /// </summary>
            [JsonProperty("title")]
            public string Title { get; set; }

            /// <summary>
            /// 摘要
            /// </summary>
            [JsonProperty("digest")]
            public string Digest { get; set; }

            /// <summary>
            /// 作者
            /// </summary>
            [JsonProperty("author")]
            public string Author { get; set; }

            /// <summary>
            /// 是否显示封面，0为不显示，1为显示
            /// </summary>
            [JsonProperty("show_cover")]
            public int ShowCover { get; set; }

            /// <summary>
            /// 封面图片的URL
            /// </summary>
            [JsonProperty("cover_url")]
            public string CoverUrl { get; set; }

            /// <summary>
            /// 正文的URL
            /// </summary>
            [JsonProperty("content_url")]
            public string ContentUrl { get; set; }

            /// <summary>
            /// 原文的URL，若置空则无查看原文入口
            /// </summary>
            [JsonProperty("source_url")]
            public string SourceUrl { get; set; }
        }
    }
}
