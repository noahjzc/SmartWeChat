using SmartWeChat.Utility;

namespace SmartWeChat.DTO
{
    public abstract class SWRequest<T> where T : SWResponse
    {

        public abstract string GetApiUrl();

        public string ToJson()
        {
            return this.JsonSerialize();
        }
    }
}
