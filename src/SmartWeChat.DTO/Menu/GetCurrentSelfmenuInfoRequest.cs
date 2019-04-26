using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SmartWeChat.DTO.Menu
{
    public class GetCurrentSelfmenuInfoRequest : SWRequest<GetCurrentSelfmenuInfoResponse>
    {
        public override string GetApiUrl()
        {
            return "/cgi-bin/get_current_selfmenu_info";
        }

        public override HttpMethod Method()
        {
            return HttpMethod.Get;
        }
    }
}
