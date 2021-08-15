using System;
using System.Text;

namespace YY.YellowPlatformFormats.Core.Helpers
{
    public static class StringExtensions
    {
        public static string RemoveBom(this string sourceValue)
        {
            string BOMMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());

            if (sourceValue.StartsWith(BOMMarkUtf8))
                sourceValue = sourceValue.Remove(0, BOMMarkUtf8.Length);

            return sourceValue.RemoveNull();
        }

        public static string AddBom(this string sourceValue)
        {
            string BOMMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());

            if (!sourceValue.StartsWith(BOMMarkUtf8, StringComparison.Ordinal))
                sourceValue = $"{BOMMarkUtf8}{sourceValue}";

            return sourceValue.RemoveNull();
        }

        public static string RemoveNull(this string sourceValue)
        {
            return  sourceValue.Replace("\0", string.Empty);
        }
    }
}
