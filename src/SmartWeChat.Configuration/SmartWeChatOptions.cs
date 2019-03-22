using System.Collections.Generic;

namespace SmartWeChat.Configuration
{
    public class SmartWeChatOptions
    {
        public string Host { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public MessageConfig ReceiveMessageConfig { get; set; }

        public IEnumerable<string> Plugins { get; set; }
    }
}