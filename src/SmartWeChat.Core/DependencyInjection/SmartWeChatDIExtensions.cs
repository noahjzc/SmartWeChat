using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SmartWeChat.Configuration;
using SmartWeChat.Core;
using SmartWeChat.Utility;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class SmartWeChatDIExtensions
    {
        public static void AddSmartWeChat(this IServiceCollection services)
        {

        }


        public static void AddSmartWeChat(this IServiceCollection services, SmartWeChatOptions setupOptions)
        {
            services.AddSingleton(setupOptions);


            foreach (string moduleName in setupOptions.Modules)
            {

            }


        }

        public static void AddSmartWeChatMessageHandler<THandler>(this IServiceCollection services, THandler handler)
            where THandler : class, ISmartWeChatMessageHandler
        {
            services.AddSingleton(handler);
            services.AddSingleton<MessageHandler>();
        }

        public static void UseMessageHandle(this IApplicationBuilder app, string path = "/wechat/receive")
        {
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
    }
}
