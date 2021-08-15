using System.Text;
using Xunit;
using YY.YellowPlatformFormats.Core.Helpers;

namespace YY.YellowPlatformFormats.Core.Tests.Helpers
{
    public class BytesHelperTests
    {
        [Fact]
        public void HexStringAndBytesArrayConverterTest()
        {
            byte[] correctByteArray = new byte[18];
            correctByteArray[0] = 2;
            correctByteArray[1] = 1;
            correctByteArray[2] = 83;
            correctByteArray[3] = 75;
            correctByteArray[4] = 111;
            correctByteArray[5] = 244;
            correctByteArray[6] = 136;
            correctByteArray[7] = 141;
            correctByteArray[8] = 193;
            correctByteArray[9] = 78;
            correctByteArray[10] = 160;
            correctByteArray[11] = 213;
            correctByteArray[12] = 235;
            correctByteArray[13] = 182;
            correctByteArray[14] = 189;
            correctByteArray[15] = 160;
            correctByteArray[16] = 167;
            correctByteArray[17] = 13;
            string sourceString = "0201534b6ff4888dc14ea0d5ebb6bda0a70d";

            byte[] sourceBytes = BytesHelper.HexStringToByteArray(sourceString);
            string convertedString = BytesHelper.ByteArrayToHexString(sourceBytes);
            
            Assert.Equal(correctByteArray.Length, sourceBytes.Length);
            Assert.Equal(sourceString, convertedString);
            for (int i = 0; i < correctByteArray.Length; i++)
            {
                byte expected = correctByteArray[i];
                byte actual = sourceBytes[i];

                Assert.True(expected == actual,
                    $"Expected: '{expected}', Actual: '{actual}' at offset {i}."
                );
            }
        }

        [Fact]
        public void ZipAndUnzipTest()
        {
            string sourceString = "From .NET with love!";
            byte[] sourceBytes = Encoding.UTF8.GetBytes(sourceString);
            byte[] compressedBytes = BytesHelper.Compress(sourceBytes);
            byte[] uncompressedBytes = BytesHelper.Decompress(compressedBytes);
            string uncompressedString = Encoding.UTF8.GetString(uncompressedBytes);

            Assert.Equal(sourceString, uncompressedString);
        }
    }
}
