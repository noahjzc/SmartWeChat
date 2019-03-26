namespace SmartWeChat.DTO.Message
{
    public class MessageBase
    {
        public string ToUserName { get; set; }

        public string FromUserName { get; set; }

        public long CreateTime { get; set; }

        public string MsgType { get; set; }
    }

    public class MessageType
    {
        public const string Text = "text";

        public const string Image = "image";

        public const string Voice = "voice";

        public const string Video = "video";

        public const string ShortVideo = "shortvideo";

        public const string Location = "location";

        public const string Link = "link";

        public const string Event = "event";
    }

    public class WeChatEvent
    {
        public const string Subscribe = "subscribe";

        public const string Unsubscribe = "unsubscribe";

        public const string Scan = "SCAN";

        public const string Location = "LOCATION";

        public const string MenuClick = "CLICK";

        public const string MenuView = "VIEW";
    }
}
