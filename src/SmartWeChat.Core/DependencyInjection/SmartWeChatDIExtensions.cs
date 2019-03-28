using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SmartWeChat.Configuration;
using SmartWeChat.Core;
using SmartWeChat.Utility;
using System;
using System.IO;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class SmartWeChatDIExtensions
    {
        private const string DEFAULT_CONFIG_PATH = "SmartWeChat.conf.json";

        public static void AddSmartWeChat(this IServiceCollection services, string configPath = DEFAULT_CONFIG_PATH)
        {
            // options
            var options = LoadOptions();
            services.AddSingleton(options);
            // http client
            AddHttpClient(services, options);
        }


        public static void AddSmartWeChat(this IServiceCollection services, SmartWeChatOptions setupOptions)
        {
            // options
            services.AddSingleton(CheckOptions(setupOptions));
            // http client
            AddHttpClient(services, setupOptions);

        }

        public static void AddSmartWeChatMessageHandler<THandler>(this IServiceCollection services, THandler handler)
            where THandler : class, ISmartWeChatMessageHandler
        {
            services.AddSingleton(handler);
            services.AddSingleton<MessageHandler>();
        }

        public static void UseMessageHandle(this IApplicationBuilder app, string path = "/wechat/receive")
        {
            var options = app.ApplicationServices.GetRequiredService<SmartWeChatOptions>();
            if (string.IsNullOrEmpty(options.ReceiveToken))
            {
                throw new SmartWeChatException("message handler can not missing ReceiveToken");
            }

            if (options.ReceiveMessageMode != ReceiveMessageMode.Clear && string.IsNullOrEmpty(options.AESKey))
            {
                throw new SmartWeChatException("can not missing AESKey when use encrypt mode or compatibility mode");
            }

            app.Map(path, builder =>
            {
                builder.Run(async context =>
                {
                    var messageHandler = app.ApplicationServices.GetRequiredService<MessageHandler>();
                    if (context.Request.Method.ToUpper() == "GET")
                    {
                        string signature = context.Request.Query["signature"];
                        string timestamp = context.Request.Query["timestamp"];
                        string nonce = context.Request.Query["nonce"];
                        string echostr = context.Request.Query["echostr"];
                        await context.Response.WriteAsync(messageHandler.CheckServer(signature, timestamp, nonce, echostr));
                    }
                    else
                    {
                        string requestBody = context.Request.Body.StreamToString();
                        string result;

                        string msgSignature = context.Request.Query["msg_signature"];
                        if (!string.IsNullOrEmpty(msgSignature))
                        {
                            string timestamp = context.Request.Query["timestamp"];
                            string nonce = context.Request.Query["nonce"];
                            result = messageHandler.Handler(requestBody, timestamp, nonce, msgSignature);
                        }
                        else
                        {
                            result = messageHandler.Handler(requestBody);
                        }
                        await context.Response.WriteAsync(result);
                    }
                });
            });
        }

        private static void AddOthers(IServiceCollection services)
        {
            services.AddMemoryCache();
        }

        private static void AddHttpClient(IServiceCollection services, SmartWeChatOptions setupOptions)
        {
            services.AddHttpClient("SmartWeChat", client => { client.BaseAddress = new Uri(setupOptions.Host); });
        }

        private static SmartWeChatOptions LoadOptions(string configPath = DEFAULT_CONFIG_PATH)
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

            return options;
        }
    }
}
