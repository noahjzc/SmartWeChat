using System;

namespace WeChatApiSDK.Core.Utility
{
    public class WeChatException : Exception
    {
        public WeChatException(Exception ex) : base("WeChatException", ex)
        {
        }
    }
}
