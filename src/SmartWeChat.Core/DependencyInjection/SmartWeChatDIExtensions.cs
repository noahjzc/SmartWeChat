using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartWeChat;
using SmartWeChat.Configuration;
using SmartWeChat.Utility;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class SmartWeChatDIExtensions
    {

        public static void AddSmartWeChat(this IServiceCollection services, string configPath = SmartWeChatConfig.DEFAULT_CONFIG_PATH, ILoggerFactory loggerFactory = null)
        {
            services.AddSingleton(new SmartWeChatRequest(configPath, loggerFactory));
        }


        public static void AddSmartWeChat(this IServiceCollection services, SmartWeChatOptions setupOptions, ILoggerFactory loggerFactory = null)
        {
            services.AddSingleton(new SmartWeChatRequest(setupOptions, loggerFactory));
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
                    var messageHandler = app.ApplicationServices.GetRequiredService<PassiveMessageProcessor>();
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


    }
}
