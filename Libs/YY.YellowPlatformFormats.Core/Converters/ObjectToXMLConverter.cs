using System;
using System.Text;
using System.Xml;
using YY.YellowPlatformFormats.Core.Types;

namespace YY.YellowPlatformFormats.Core.Converters
{
    public static class ObjectToXMLConverter
    {
        public static string ConvertToXMLString(object sourceObject)
        {
            Type sourceObjectType = sourceObject.GetType();

            if(sourceObjectType == typeof(ValueStorage))
            {
                byte[] data = ((ValueStorage)sourceObject).AsBytes();
                string valueAsBase64 = Convert.ToBase64String(data);

                StringBuilder xmlStringBuilder = new StringBuilder();
                xmlStringBuilder.Append(@"<ValueStorage xmlns=""http://v8.1c.ru/data"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""ValueStorage"">");
                xmlStringBuilder.Append(valueAsBase64);
                xmlStringBuilder.Append(@"</ValueStorage>");

                return xmlStringBuilder.ToString();
            }

            throw new NotImplementedException();                        
        }

        public static XmlDocument ConvertToXML(object sourceObject)
        {
            string xmlString = ConvertToXMLString(sourceObject);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);

            return xmlDocument;
        }
    }
}
