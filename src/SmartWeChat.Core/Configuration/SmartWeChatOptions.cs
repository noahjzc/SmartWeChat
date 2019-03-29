using System.Collections.Generic;

namespace SmartWeChat.Configuration
{
    public class SmartWeChatOptions
    {
        public string Host { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public IEnumerable<string> Modules { get; set; }

        public bool UsePassiveMessageProcessor { get; set; } = false;

        public string ReceiveToken { get; set; }

        public string ReceiveMessageMode { get; set; } = "Clear";

        public string AESKey { get; set; }
    }
}