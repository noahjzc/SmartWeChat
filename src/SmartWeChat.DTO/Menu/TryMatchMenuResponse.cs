using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartWeChat.DTO.Menu
{
    public class TryMatchMenuResponse : SWResponse
    {
        [JsonProperty("button")]
        public IEnumerable<RootButtonItem> Button { get; set; }
    }
}
