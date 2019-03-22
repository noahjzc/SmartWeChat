namespace SmartWeChat.Configuration
{
    public enum ReceiveMessageMode
    {
        /// <summary>
        /// 明文
        /// </summary>
        Clear = 1 << 0,

        /// <summary>
        /// 兼容
        /// </summary>
        Compatibility = 1 << 1,

        /// <summary>
        /// 加密
        /// </summary>
        Encrypt = 1 << 2,
    }
}
