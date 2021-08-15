using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace YY.YellowPlatformFormats.Core.Helpers
{
    public static class BytesHelper
    {
        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] HexStringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static byte[] Compress(byte[] bytes)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(bytes, 0, bytes.Length);
            }
            return output.ToArray();
        }

        public static byte[] Decompress(byte[] bytes)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var compressStream = new MemoryStream(bytes))
                {
                    using (var deflateStream = new DeflateStream(compressStream, CompressionMode.Decompress))
                    {
                        deflateStream.CopyTo(outputStream);
                    }
                }

                return outputStream.ToArray();
            }
        }
    }
}
