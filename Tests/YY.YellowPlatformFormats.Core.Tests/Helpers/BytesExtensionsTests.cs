using Xunit;
using YY.YellowPlatformFormats.Core.Helpers;

namespace YY.YellowPlatformFormats.Core.Tests.Helpers
{
    public class BytesExtensionsTests
    {
        [Fact]
        public void RemoveNullsTest()
        {
            byte[] bytes = new byte[3];
            bytes[0] = 1;
            bytes[1] = (byte)'\0';
            bytes[2] = 0;

            byte[] bytesClear = bytes.RemoveNulls();

            Assert.Single(bytesClear);
            Assert.Equal(1, bytesClear[0]);
        }
    }
}
