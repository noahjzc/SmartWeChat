using System;
using System.Collections.Generic;
using System.Text;
using SmartWeChat.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
   public  static class SmartWeChatDIExtensions
    {
        public static void AddSmartWeChat(this IServiceCollection services,Func<IServiceProvider,SmartWeChatOptions> setupOptions)
        {
        }
    }
}
