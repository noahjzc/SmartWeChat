using SmartWeChat.DTO.Menu;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartWeChat.Testing
{
    public class MenuTest
    {
        private readonly SmartWeChatRequest _smartWeChatRequest = new SmartWeChatRequest();

        [Fact]
        public void CreateMenu()
        {
            var buttons = new List<RootButtonItem>
            {
                new RootButtonItem { Name = "菜单1", Type = MenuTypeEnum.Click, Key = "MENU_1"},
                new RootButtonItem { Name = "有子菜单", SubButton = new List<ButtonItem>
                    {
                        new ButtonItem { Name = "子菜单1", Type = MenuTypeEnum.View, Url = "http://baidu.com" },
                        new ButtonItem { Name = "子菜单2", Type = MenuTypeEnum.View, Url = "http://qq.com" }
                    }
                }
            };
            var request = new CreateMenuRequest
            {
                Button = buttons
            };

            var rtn = _smartWeChatRequest.Execute(request).Result;
            Assert.Equal(0, rtn.ErrorCode);
        }

        [Fact]
        public void GetMenu()
        {
            var request = new GetMenuRequest();
            var rtn = _smartWeChatRequest.Execute(request).Result;
            Assert.Equal(0, rtn.ErrorCode);
            Assert.Equal(2, rtn.Menu.Button.Count());
        }

    }

}
