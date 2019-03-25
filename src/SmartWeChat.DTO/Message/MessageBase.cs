namespace SmartWeChat.DTO.Message
{
    public class MessageBase
    {
        public string ToUserName { get; set; }

        public string FromUserName { get; set; }

        public long CreateTime { get; set; }

        public string MsgType { get; set; }
    }
}
