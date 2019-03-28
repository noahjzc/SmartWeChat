using Microsoft.Extensions.Logging;
using SmartWeChat.Configuration;
using SmartWeChat.DTO.Message;
using SmartWeChat.Utility;
using SmartWeChat.Utility.Encrypt;
using System;
using System.Text;

namespace SmartWeChat.Core
{
    public interface ISmartWeChatMessageHandler
    {
        ReplyMessageModelBase Handle(MessageBase message);
    }

    public class MessageHandler
    {
        private readonly Func<MessageBase, ReplyMessageModelBase> _customHandle;
        private readonly SmartWeChatOptions _options;
        private readonly ILogger<MessageHandler> _logger;

        public MessageHandler(SmartWeChatOptions options, Func<MessageBase, ReplyMessageModelBase> handler, ILogger<MessageHandler> sLogger)
        {
            _options = options;
            _customHandle = handler;
            _logger = sLogger;
        }

        public MessageHandler(ISmartWeChatMessageHandler _handler, SmartWeChatOptions sOptions, ILogger<MessageHandler> sLogger)
        {
            _options = sOptions;
            _logger = sLogger;
            _customHandle = _handler.Handle;
        }

        public string CheckServer(string signature, string timestamp, string nonce, string echostr)
        {
            _logger.LogDebug("start check server");
            _logger.LogDebug($"signature = {signature}");
            _logger.LogDebug($"timestamp = {timestamp}");
            _logger.LogDebug($"nonce = {nonce}");
            _logger.LogDebug($"echostr = {echostr}");

            string[] sortString = { _options.ReceiveToken, timestamp, nonce };

            Array.Sort(sortString);

            string content = string.Join("", sortString);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(content));
            string sha1String = Encoding.UTF8.GetString(sha1Bytes);
            sha1String = sha1String.ToLower();

            return signature == sha1String ? echostr : string.Empty;
        }

        public string Handler(string requestBody, string timestamp, string nonce, string msgSignature)
        {
            string clearMessage = string.Empty;

            var encryptMessage = requestBody.XmlDeserialize<HandleMessageModelBase>();
            if (string.IsNullOrEmpty(encryptMessage.Encrypt))
            {
                throw new SmartWeChatException("密文为空");
            }

            var crypt = new WXBizMsgCrypt(_options);
            int rtn = crypt.DecryptMsg(msgSignature, timestamp, nonce, requestBody, ref clearMessage);

            if (rtn != 0)
            {
                throw new SmartWeChatException("解密失败");
            }

            return Handler(clearMessage);
        }

        public string Handler(string requestBody, bool needEncrypt = false)
        {
            var message = Parse(requestBody);
            try
            {
                string result = _customHandle(message).ToString();

                if (needEncrypt)
                {
                    var crypt = new WXBizMsgCrypt(_options);
                    string timestamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds.ToString("N0");
                    string nonce = Guid.NewGuid().ToString("N");
                    string cryptograph = string.Empty;
                    int encryptResult = crypt.EncryptMsg(result, timestamp, nonce, ref cryptograph);
                    if (encryptResult != 0)
                    {
                        throw new SmartWeChatException("加密失败");
                    }
                    return cryptograph;
                }

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(new SmartWeChatException(e), "handle failure");
                return "failure";
            }
        }


        private MessageBase Parse(string xmlString)
        {
            var message = xmlString.XmlDeserialize<MessageBase>();

            if (string.Equals(message.MsgType, MessageType.Event))
            {
                var @event = xmlString.XmlDeserialize<HandleEventMessageModelBase>();
                switch (@event.Event)
                {
                    case WeChatEvent.Location:
                        return xmlString.XmlDeserialize<HandleLocationEventModel>();
                    case WeChatEvent.MenuClick:
                        return xmlString.XmlDeserialize<HandleMenuClickedEventModel>();
                    case WeChatEvent.MenuView:
                        return xmlString.XmlDeserialize<HandleMenuClickedEventModel>();
                    case WeChatEvent.Subscribe:
                        return xmlString.XmlDeserialize<HandleSubscribeEventModel>();
                    case WeChatEvent.Unsubscribe:
                        return xmlString.XmlDeserialize<HandleSubscribeEventModel>();
                    case WeChatEvent.Scan:
                        return xmlString.XmlDeserialize<HandleQRCodeScanEventModel>();
                    default:
                        throw new SmartWeChatException($"Unknow Event => {@event.Event}");
                }
            }
            else
            {
                switch (message.MsgType)
                {
                    case MessageType.Text:
                        return xmlString.XmlDeserialize<HandleTextMessageModel>();
                    case MessageType.Voice:
                        return xmlString.XmlDeserialize<HandleVoiceMessageModel>();
                    case MessageType.Image:
                        return xmlString.XmlDeserialize<HandleImageMessageModel>();
                    case MessageType.Video:
                        return xmlString.XmlDeserialize<HandleVoiceMessageModel>();
                    case MessageType.ShortVideo:
                        return xmlString.XmlDeserialize<HandleVoiceMessageModel>();
                    case MessageType.Location:
                        return xmlString.XmlDeserialize<HandleLocationMessageModel>();
                    case MessageType.Link:
                        return xmlString.XmlDeserialize<HandleLinkMessageModel>();
                    default:
                        throw new SmartWeChatException($"Unknow Message Type => {message.MsgType}");
                }
            }


        }

    }
}
