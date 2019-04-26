using SmartWeChat.DTO.Tools;
using Xunit;

namespace SmartWeChat.Testing
{
    public class SmartWeChatTest
    {
        private readonly SmartWeChatRequest _smartWeChatRequest = new SmartWeChatRequest();
        
        [Fact]
        public void NetCheck()
        {
            var request = new NetCheckRequest
            {
                Action = NetCheckRequest.NetCheckAction.ALL,
                CheckOperator = NetCheckRequest.NetCheck_CheckOperator.DEFAULT
            };

            var rtn = _smartWeChatRequest.Execute(request).Result;
            Assert.Equal(0, rtn.ErrorCode);
            Assert.NotNull(rtn.Ping);
        }
    }
}
