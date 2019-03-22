using System;

namespace SmartWeChat.Utility
{
    public class WeChatException : Exception
    {
        public WeChatException(Exception ex) : base("WeChatException", ex)
        {
        }
    }
}
