using System;
using System.Collections.Generic;
using System.Text;

namespace SmartWeChat.Configuration
{
   public class MessageConfig
    {
        public string ReceiveToken { get; set; }

        public ReceiveMessageMode ReceiveMessageMode { get; set; } = ReceiveMessageMode.Clear;

        public string AESKey { get; set; }
    }
}
