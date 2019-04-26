using System.Net.Http;

namespace SmartWeChat.DTO.Menu
{
    public class GetMenuRequest : SWRequest<GetMenuResponse>
    {
        public override string GetApiUrl()
        {
            return "/cgi-bin/menu/get";
        }

        public override HttpMethod Method()
        {
            return HttpMethod.Get;
        }
    }
}
 