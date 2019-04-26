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
    public class SmartWeChatConfig
    {
        internal const string DEFAULT_CONFIG_PATH = "SmartWeChat.conf.json";

        internal ILoggerFactory LoggerFactory { get; private set; } = NullLoggerFactory.Instance;
        internal IHttpClientFactory HttpClientFactory { get; private set; }
        public SmartWeChatOptions Options { get; private set; }
        internal DefaultRequest DefaultRequest { get; private set; }
        internal TokenManager TokenManager { get; private set; }
        internal PassiveMessageProcessor PassiveMessageProcessor { get; private set; }
        internal WXBizMsgCrypt WxBizMsgCrypt { get; private set; }

        public SmartWeChatConfig(string configPath)
        {
            Options = LoadOptions(configPath);
            InitConfig();
        }

        public SmartWeChatConfig(SmartWeChatOptions options, ILoggerFactory loggerFactory = null)
        {
            if (loggerFactory != null)
            {
                LoggerFactory = loggerFactory;
            }

            Options = CheckOptions(options);
            InitConfig();
        }

        public void UseLog(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }

        private void InitConfig()
        {
            HttpClientFactory = new SmartWeChatHttpClientFactory(Options);
            DefaultRequest = new DefaultRequest(LoggerFactory.CreateLogger<DefaultRequest>(), HttpClientFactory);
            TokenManager = new TokenManager(LoggerFactory.CreateLogger<TokenManager>(), Options, DefaultRequest);

            if (Options.UsePassiveMessageProcessor)
            {
                WxBizMsgCrypt = new WXBizMsgCrypt(Options);
                PassiveMessageProcessor = new PassiveMessageProcessor(Options, LoggerFactory);
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
                options.Host = "https://api.weixin.qq.com";
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
