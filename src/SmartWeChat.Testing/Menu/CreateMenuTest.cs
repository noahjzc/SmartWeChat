using System.Collections.Generic;
using SmartWeChat.DTO.Menu;
using Xunit;

namespace SmartWeChat.Testing.Menu
{
    public class CreateMenuTest
    {
        [Fact]
        public void ModelValidateTest()
        {
            var firstButtonItems = new List<CreateMenuRequest.ButtonItem>
            {
                new CreateMenuRequest.ButtonItem
                {
                    Type = MenuTypeEnum.View,
                    Url = "http://aaa.com",
                    Name = "A"
                },
                new CreateMenuRequest.ButtonItem
                {
                    Type = MenuTypeEnum.Click,
                    Name = "B"
                }
            };

            var rootButtons = new List<CreateMenuRequest.RootButtonItem>
            {
                new CreateMenuRequest.RootButtonItem
                {
                    Name = "First Root Menu",
                    SubButton = firstButtonItems
                }
            };

            var createMenuRequest = new CreateMenuRequest
            {
                Button = rootButtons
            };

        }
    }
}
