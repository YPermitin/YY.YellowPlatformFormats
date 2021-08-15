using System.Linq;
using System.Text;
using Xunit;
using YY.YellowPlatformFormats.Core.Helpers;

namespace YY.YellowPlatformFormats.Core.Tests.Helpers
{
    public class StringExtensionsTests
    {
        [Fact]
        public void RemoveBomTest()
        {
            string sourceString = "From .NET with love!";
            var data = Encoding.UTF8.GetBytes(sourceString);
            var result = Encoding.UTF8.GetPreamble().Concat(data).ToArray();
            var encoder = new UTF8Encoding(true);
            
            string resultString = encoder.GetString(result);
            byte[] resultBytes = encoder.GetBytes(resultString);
            byte[] UTF8_BOM = Encoding.UTF8.GetPreamble();

            string clearStringFromBOM = resultString.RemoveBom();

            Assert.NotEqual(sourceString, resultString);
            Assert.Equal(UTF8_BOM[0], resultBytes[0]);
            Assert.Equal(UTF8_BOM[1], resultBytes[1]);
            Assert.Equal(UTF8_BOM[2], resultBytes[2]);
            Assert.Equal(sourceString, clearStringFromBOM);
        }

        [Fact]
        public void AddBomTest()
        {
            string sourceString = "From .NET with love!";
            string resultString = sourceString.AddBom();
            var encoder = new UTF8Encoding(true);
            byte[] resultBytes = encoder.GetBytes(resultString);
            byte[] UTF8_BOM = Encoding.UTF8.GetPreamble();

            Assert.NotEqual(sourceString, resultString);
            Assert.Equal(UTF8_BOM[0], resultBytes[0]);
            Assert.Equal(UTF8_BOM[1], resultBytes[1]);
            Assert.Equal(UTF8_BOM[2], resultBytes[2]);
        }

        [Fact]
        public void RemoveNullTest()
        {
            string sourceValue = "\0\0\0\0\0Some value\0\0\0\0\0\0\0\0\0\0";
            string resultValue = sourceValue.RemoveNull();

            Assert.Equal("Some value", resultValue);
        }
    }
}
