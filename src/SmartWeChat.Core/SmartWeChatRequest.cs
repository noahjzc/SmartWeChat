using Microsoft.Extensions.Logging;
using SmartWeChat.Configuration;
using SmartWeChat.DTO;
using System.Threading.Tasks;

namespace SmartWeChat
{
    public class SmartWeChatRequest
    {
        public SmartWeChatConfig SmartWeChatConfig { get; private set; }

        public SmartWeChatRequest(string configPath = SmartWeChatConfig.DEFAULT_CONFIG_PATH, ILoggerFactory loggerFactory = null)
        {
            SmartWeChatConfig = new SmartWeChatConfig(configPath);
            if (loggerFactory != null)
            {
                SmartWeChatConfig.UseLog(loggerFactory);
            }
        }

        public SmartWeChatRequest(SmartWeChatOptions options, ILoggerFactory loggerFactory = null)
        {
            SmartWeChatConfig = new SmartWeChatConfig(options, loggerFactory);
        }

        public async Task<TResponse> Execute<TResponse>(SWRequest<TResponse> request) where TResponse : SWResponse
        {
            return await SmartWeChatConfig.DefaultRequest.Execute(request,
                await SmartWeChatConfig.TokenManager.GetTokenAsync());
        }
    }
}
