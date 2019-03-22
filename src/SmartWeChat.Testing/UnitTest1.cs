using Xunit;

namespace SmartWeChat.Testing
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int i = 1;
            i++;
            Assert.Equal(2, i);
        }
    }
}
