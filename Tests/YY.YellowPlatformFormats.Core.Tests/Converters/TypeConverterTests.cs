using System;
using Xunit;
using YY.YellowPlatformFormats.Core.Converters;

namespace YY.YellowPlatformFormats.Core.Tests.Converters
{
    public class TypeConverterTests
    {
        [Theory]
        [ClassData(typeof(TypeConverterGetTypeByIdTestsData))]
        public void GetTypeByIdTest(object value)
        {
            Type valueType = value.GetType();
            var typeId = TypeConverter.GetTypeIdByType(valueType);
            var typeFountById = TypeConverter.GetTypeById(typeId);

            Assert.Equal(valueType, typeFountById);
        }

        [Theory]
        [ClassData(typeof(TypeConverterConvertToTestsData))]
        public void ConvertToTests(object sourceValue, Type destinationType)
        {
            var convertedValue = TypeConverter.ConvertTo(sourceValue, destinationType);
            var convertedValueType = convertedValue.GetType();

            Assert.Equal(destinationType, convertedValueType);
        }
    }
}
