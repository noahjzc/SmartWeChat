using SmartWeChat.Configuration;
using System;

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
    }
}
