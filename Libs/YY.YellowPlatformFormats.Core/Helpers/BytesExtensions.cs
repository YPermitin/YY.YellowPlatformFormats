using System.Linq;

namespace YY.YellowPlatformFormats.Core.Helpers
{
    public static class BytesExtensions
    {
        public static byte[] RemoveNulls(this byte[] sourceValue)
        {
            return sourceValue
                .Where(v => v != '\0')
                .ToArray();
        }
    }
}
