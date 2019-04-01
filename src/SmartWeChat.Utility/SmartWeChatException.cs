using System;

namespace SmartWeChat.Utility
{
    public class SmartWeChatException : Exception
    {
        public SmartWeChatException(string message, Exception innerException = null) : base(message, innerException) { }

        public SmartWeChatException(Exception innerException) : this("SmartWeChatException", innerException)
        {
        }
    }
}
