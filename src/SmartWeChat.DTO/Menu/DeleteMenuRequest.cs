using System.Net.Http;

namespace SmartWeChat.DTO.Menu
{
    public class DeleteMenuRequest : SWRequest<SWResponse>
    {
        public override string GetApiUrl()
        {
            return "/cgi-bin/menu/delete";
        }

        public override HttpMethod Method()
        {
            return HttpMethod.Get;
        }
    }
}
