using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SmartWeChat.Configuration;
using SmartWeChat.Encrypt;
using SmartWeChat.Utility;
using System;
using System.IO;
using System.Net.Http;

namespace SmartWeChat
{
    public class SmartWeChatContainer
    {
        public static SmartWeChatContainer GetInstance()
        {
            return GetInstance("");
        }

        public static SmartWeChatContainer GetInstance(string configPath)
        {
            return new SmartWeChatContainer(configPath);
        }

        public static SmartWeChatContainer GetInstance(SmartWeChatOptions options, ILoggerFactory loggerFactory = null)
        {
            return new SmartWeChatContainer(options, loggerFactory);
        }

        const string DEFAULT_CONFIG_PATH = "SmartWeChat.conf.json";

        public ILoggerFactory LoggerFactory { get; private set; } = NullLoggerFactory.Instance;
        public IHttpClientFactory HttpClientFactory { get; private set; }
        public SmartWeChatOptions Options { get; private set; }
        public DefaultRequest DefaultRequest { get; private set; }
        public TokenManager TokenManager { get; private set; }
        public PassiveMessageProcessor PassiveMessageProcessor { get; private set; }
        public WXBizMsgCrypt WxBizMsgCrypt { get; private set; }

        public SmartWeChatContainer(string configPath = DEFAULT_CONFIG_PATH)
        {
            Options = LoadOptions(configPath);
            InitConfig();
        }

        public SmartWeChatContainer(SmartWeChatOptions options, ILoggerFactory loggerFactory = null)
        {
            if (loggerFactory != null)
            {
                LoggerFactory = loggerFactory;
            }

            Options = CheckOptions(options);
            InitConfig();
        }

        private void InitConfig()
        {
            HttpClientFactory = new SmartWeChatHttpClientFactory(Options);
            DefaultRequest = new DefaultRequest(LoggerFactory.CreateLogger<DefaultRequest>(), HttpClientFactory);
            TokenManager = new TokenManager(LoggerFactory.CreateLogger<TokenManager>(), Options, DefaultRequest);

            if (Options.UsePassiveMessageProcessor)
            {
                WxBizMsgCrypt=new WXBizMsgCrypt(Options);
                PassiveMessageProcessor=new PassiveMessageProcessor();
            }
        }

        private static SmartWeChatOptions LoadOptions(string configPath)
        {
            if (!File.Exists(configPath))
            {
                throw new SmartWeChatException("config file not found");
            }

            try
            {
                string jsonString = File.ReadAllText(configPath);
                var options = jsonString.JsonDeserialize<SmartWeChatOptions>();
                return CheckOptions(options);
            }
            catch (Exception e)
            {
                throw new SmartWeChatException("config file load failure", e);
            }
        }

        private static SmartWeChatOptions CheckOptions(SmartWeChatOptions options)
        {
            if (string.IsNullOrEmpty(options.Host))
            {
                options.Host = "api.weixin.qq.com";
            }

            if (string.IsNullOrEmpty(options.AppId))
            {
                throw new SmartWeChatException("app id not found");
            }

            if (string.IsNullOrEmpty(options.AppSecret))
            {
                throw new SmartWeChatException("app secret not found");
            }

            if (options.UsePassiveMessageProcessor && string.IsNullOrEmpty(options.ReceiveToken))
            {
                throw new SmartWeChatException("Passive Message Processor can not missing ReceiveToken");
            }

            if (options.UsePassiveMessageProcessor && options.ReceiveMessageMode != ReceiveMessageMode.Clear && string.IsNullOrEmpty(options.AESKey))
            {
                throw new SmartWeChatException("can not missing AESKey when use encrypt mode or compatibility mode");
            }

            return options;
        }
    }
}
