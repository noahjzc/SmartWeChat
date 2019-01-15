using System.Threading.Tasks;

namespace WeChatApiSDK.Core
{
    public interface IRequest
    {
        Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(string url, TRequest reqMsg);

        Task<TResponse> GetAsJsonAsync<TResponse>(string url);
    }
}
