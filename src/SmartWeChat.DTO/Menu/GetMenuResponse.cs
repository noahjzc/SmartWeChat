using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartWeChat.DTO.Menu
{
    public class GetMenuResponse : SWResponse
    {
        [JsonProperty("menu")]
        public Menus Menu { get; set; }

        [JsonProperty("conditionalmenu")]
        public Menus ConditionalMenu { get; set; }
    }

    public class Menus
    {
        [JsonProperty("button")]
        public IEnumerable<RootButtonItem> Button { get; set; }
    }
}
