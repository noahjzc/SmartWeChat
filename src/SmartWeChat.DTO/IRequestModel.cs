using SmartWeChat.Utility;

namespace SmartWeChat.DTO
{
    public class IRequestModel
    {
        public override string ToString()
        {
            return this.JsonSerialize();
        }
    }
}
