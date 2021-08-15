using System;
using System.Xml;
using Xunit;
using YY.YellowPlatformFormats.Core.Converters;
using YY.YellowPlatformFormats.Core.Types;

namespace YY.YellowPlatformFormats.Core.Tests.Converters
{
    public class ObjectToXMLConverterTests
    {
        [Fact]
        public void ValueStorage_ConvertToXMLStringTest()
        {
            string sourceValue = "From .NET with love!";
            ValueStorage storage = ValueStorage.FromValue(sourceValue);

            string xmlString = ObjectToXMLConverter.ConvertToXMLString(storage);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            byte[] valueAsBytes = Convert.FromBase64String(xmlDocument.FirstChild?.InnerText ?? string.Empty);

            ValueStorage extractedStorage = new ValueStorage(valueAsBytes);
            string valueAsString = extractedStorage.GetRawValueAsString();

            string correctInternalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(correctInternalString, valueAsString);
        }
    }
}
