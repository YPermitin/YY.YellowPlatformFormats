using System;
using System.Linq;
using System.Text;
using OneSTools.BracketsFile;
using YY.YellowPlatformFormats.Core.Converters;
using YY.YellowPlatformFormats.Core.Helpers;

namespace YY.YellowPlatformFormats.Core.Types
{
    public class ValueStorage : BaseType
    {
        public static ValueStorage FromValue(object value)
        {
            var valueString = ObjectToInternalStringConverter.ValueToStringInternal(value);
            var valueBytes = Encoding.UTF8.GetBytes(valueString);
            int valueToSendSize = valueBytes.Length + Encoding.UTF8.GetPreamble().Length;
            byte[] valueToSendSizeAsBytes = BitConverter.GetBytes(valueToSendSize);
            byte[] valueReady = new byte[10 + 3 + valueBytes.Length];
            valueReady[0] = 1;
            valueReady[1] = 1;
            Array.Copy(valueToSendSizeAsBytes, 0, valueReady, 2, valueToSendSizeAsBytes.Length);
            Array.Copy(Encoding.UTF8.GetPreamble(), 0, valueReady, 10, Encoding.UTF8.GetPreamble().Length);
            Array.Copy(valueBytes, 0, valueReady, 13, valueBytes.Length);

            return new ValueStorage(valueReady);
        }

        private static readonly uint _dataHeaderSize = 8;
        private static readonly uint _dataWithCompressionHeaderSize = 18;
        private static readonly uint _headerSizeGZIP = 18;
        private static readonly byte[] _headerGZIP;

        static ValueStorage()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            _headerGZIP = BytesHelper.HexStringToByteArray("0201534b6ff4888dc14ea0d5ebb6bda0a70d");
        }

        private readonly byte[] _data;
        private bool? _isCompressed;
        private uint? _dataSize;

        public bool IsCompressed
        {
            get
            {
                if (_isCompressed == null)
                {
                    if (_data.Length >= _headerSizeGZIP)
                    {
                        byte[] gzipDataHeader = new byte[_headerSizeGZIP];
                        Array.Copy(_data, 0, gzipDataHeader, 0, _headerSizeGZIP);
                        _isCompressed = gzipDataHeader.SequenceEqual(_headerGZIP);
                    }
                    else
                    {
                        _isCompressed = false;
                    }
                }

                return (bool)_isCompressed;
            }
        }

        public uint Size
        {
            get
            {
                if (_dataSize == null)
                {
                    _dataSize = (uint)GetRawValueAsBytes().Length;
                }

                return (uint)_dataSize;
            }
        }

        public ValueStorage(byte[] data)
        {
            if (data.Length < _dataHeaderSize)
                throw new ArgumentException($"Bytes array size for value storage should by greater or equal {_dataHeaderSize}");

            _data = data;
        }

        public ValueStorage(BracketsNode structureData)
        {
            throw new NotImplementedException();
        }

        public byte[] GetRawHeaderAsBytes()
        {
            byte[] unzipValueStorageData = UnzipValueStorage();
            byte[] valueStorageHeader = new byte[_dataHeaderSize];
            Array.Copy(unzipValueStorageData, 0, valueStorageHeader, 0, valueStorageHeader.Length);

            return valueStorageHeader;
        }

        public byte[] GetRawValueAsBytes()
        {
            byte[] unzipValueStorageData = UnzipValueStorage();
            byte[] valueStorageData = new byte[unzipValueStorageData.Length - _dataHeaderSize];
            Array.Copy(unzipValueStorageData, _dataHeaderSize, valueStorageData, 0, valueStorageData.Length);

            return valueStorageData.RemoveNulls();
        }

        public string GetRawValueAsString()
        {
            byte[] valueAsBytes = GetRawValueAsBytes();

            return Encoding.UTF8
                .GetString(valueAsBytes)
                .Trim()
                .RemoveNull()
                .RemoveBom();
        }

        public byte[] AsBytes()
        {
            return _data;
        }

        public string AsStringXML()
        {
            return ObjectToXMLConverter.ConvertToXMLString(this);
        }

        private byte[] UnzipValueStorage()
        {
            byte[] valueStorageData;
            if (IsCompressed)
            {
                byte[] headerWithCompression = new byte[_dataWithCompressionHeaderSize];
                byte[] storageDataWithCompression = new byte[_data.Length - _dataWithCompressionHeaderSize];
                Array.Copy(_data, 0, headerWithCompression, 0, headerWithCompression.Length);
                Array.Copy(_data, _dataWithCompressionHeaderSize, storageDataWithCompression, 0, storageDataWithCompression.Length);
                valueStorageData = BytesHelper.Decompress(storageDataWithCompression);
            }
            else
            {
                valueStorageData = _data;
            }

            return valueStorageData;
        }
    }
}
