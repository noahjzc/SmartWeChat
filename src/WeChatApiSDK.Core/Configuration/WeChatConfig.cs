namespace WeChatApiSDK.Core.Configuration
{
    public class WeChatConfig
    {
        public string Host { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string ReceiveToken { get; set; }

        public ReceiveMessageMode ReceiveMessageMode { get; set; }

        public string AESKey { get; set; }
    }
}
