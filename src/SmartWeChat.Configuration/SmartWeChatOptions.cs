using System.Collections.Generic;

namespace SmartWeChat.Configuration
{
    public class SmartWeChatOptions
    {
        public string Host { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public IEnumerable<string> Modules { get; set; }

        public string ReceiveToken { get; set; }

        public ReceiveMessageMode ReceiveMessageMode { get; set; } = ReceiveMessageMode.Clear;

        public string AESKey { get; set; }
    }
}