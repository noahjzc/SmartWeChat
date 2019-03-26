using System;

namespace SmartWeChat.Utility
{
    public class SmartWeChatException : Exception
    {
        public SmartWeChatException(string message) : base(message) { }

        public SmartWeChatException(Exception ex) : base("SmartWeChatException", ex)
        {
        }
    }
}
