using System;

namespace SmartWeChat.Utility
{
    public class SmartWeChatException : Exception
    {
        public SmartWeChatException(string message, Exception innerException = null) : base(message, ex) { }

        public SmartWeChatException(Exception innerException) : this("SmartWeChatException", ex)
        {
        }
    }
}
