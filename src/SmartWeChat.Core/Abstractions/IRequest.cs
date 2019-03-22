using System.Threading.Tasks;
using SmartWeChat.DTO;

namespace SmartWeChat.Core.Abstractions
{
    public interface IRequest
    {
        Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(string url, TRequest reqMsg)
            where TResponse : ResponseBase where TRequest : IRequestModel;

        Task<TResponse> GetAsJsonAsync<TResponse>(string url) where TResponse : ResponseBase;
    }
}
