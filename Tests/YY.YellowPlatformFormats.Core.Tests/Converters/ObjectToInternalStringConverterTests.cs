using System;
using Xunit;
using YY.YellowPlatformFormats.Core.Converters;
using YY.YellowPlatformFormats.Core.Types;

namespace YY.YellowPlatformFormats.Core.Tests.Converters
{
    public class ObjectToInternalStringConverterTests
    {
        [Fact]
        public void StringToStringInternal()
        {
            string sourceValue = "From .NET with love!";
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""S"",""From .NET with love!""}", internalString);
        }

        [Fact]
        public void DateTimeToStringInternal()
        {
            DateTime sourceValue = new DateTime(2021,1,19, 23,12,10);
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""D"",20210119231210}", internalString);
        }

        [Fact]
        public void BoolTimeToStringInternal()
        {
            bool sourceValue = true;
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""B"",1}", internalString);
        }

        [Fact]
        public void DecimalToStringInternal()
        {
            decimal sourceValue = 145.54M;
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""N"",145.54}", internalString);
        }

        [Fact]
        public void UndefinedToStringInternal()
        {
            UndefinedType sourceValue = UndefinedType.Default;
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""U""}", internalString);
        }

        [Fact]
        public void NullToStringInternal()
        {
            NullValue sourceValue = NullValue.Default;
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""L""}", internalString);
        }

        [Fact]
        public void GuidToStringInternal()
        {
            Guid sourceValue = new Guid("4238019d-7e49-4fc9-91db-b6b951d5cf8e");
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""#"",fc01b5df-97fe-449b-83d4-218a090e681e,4238019d-7e49-4fc9-91db-b6b951d5cf8e}", internalString);
        }

        [Fact]
        public void ObjectsArrayToStringInternal()
        {
            ObjectsArray sourceArray = new ObjectsArray();
            sourceArray.Add(111M);
            sourceArray.Add("From .NET with love!");
            ObjectsArray sourceValue = sourceArray;
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""#"",51e7a0d2-530b-11d4-b98a-008048da3034,{2,{""N"",111.00},{""S"",""From .NET with love!""}}}", internalString);
        }

        [Fact]
        public void FixedObjectsArrayToStringInternal()
        {
            ObjectsArray sourceArray = new ObjectsArray();
            sourceArray.Add(111M);
            sourceArray.Add("From .NET with love!");
            FixedObjectsArray sourceValue = new FixedObjectsArray(sourceArray);
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""#"",4500381b-db30-4a10-9db4-990038032acf,{2,{""N"",111.00},{""S"",""From .NET with love!""}}}", internalString);
        }

        [Fact]
        public void StructureToStringInternal()
        {
            Structure sourceValue = new Structure();
            sourceValue.Insert("Number", 111M);
            sourceValue.Insert("String", "From .NET with love!");
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""#"",4238019d-7e49-4fc9-91db-b6b951d5cf8e,{2,{{""S"",""Number""},{""N"",111.00}},{{""S"",""String""},{""S"",""From .NET with love!""}}}}", internalString);
        }

        [Fact]
        public void FixedStructureToStringInternal()
        {
            Structure sourceValue = new Structure();
            sourceValue.Insert("Number", 111M);
            sourceValue.Insert("String", "From .NET with love!");
            FixedStructure sourceValueFixed = new FixedStructure(sourceValue);
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValueFixed);

            Assert.Equal(@"{""#"",3ee983d7-ace7-40f9-bb7e-2e916fcddd56,{2,{{""S"",""Number""},{""N"",111.00}},{{""S"",""String""},{""S"",""From .NET with love!""}}}}", internalString);
        }

        [Fact]
        public void MapToStringInternal()
        {
            Map sourceValue = new Map();
            sourceValue.Insert(123456, 111M);
            sourceValue.Insert("String", "From .NET with love!");
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(sourceValue);

            Assert.Equal(@"{""#"",3d48feae-a9c6-4c5a-a099-9eb6477630c6,{2,{{""L""},{""N"",111.00}},{{""S"",""String""},{""S"",""From .NET with love!""}}}}", internalString);
        }

        [Fact]
        public void FixedMapToStringInternal()
        {
            Map sourceValue = new Map();
            sourceValue.Insert(123456, 111M);
            sourceValue.Insert("String", "From .NET with love!");
            FixedMap fixedSourceValue = new FixedMap(sourceValue);
            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(fixedSourceValue);

            Assert.Equal(@"{""#"",220455ea-6c85-4513-996f-bbe79ed07774,{2,{{""L""},{""N"",111.00}},{{""S"",""String""},{""S"",""From .NET with love!""}}}}", internalString);
        }

        [Fact]
        public void ComplexObjectToInternalString_v1_Test()
        {
            Structure value = new Structure();
            value.Insert("Тест1", 1M);
            value.Insert("Тест2", "Какая-то строка");
            Structure value2 = new Structure();
            value2.Insert("Знач1", NullValue.Default);
            value2.Insert("Знач2", UndefinedType.Default);
            value.Insert("Объект", value2);

            string internalString = ObjectToInternalStringConverter.ValueToStringInternal(value);
            Assert.Equal(
                @"{""#"",4238019d-7e49-4fc9-91db-b6b951d5cf8e,{3,{{""S"",""Тест1""},{""N"",1.00}},{{""S"",""Тест2""},{""S"",""Какая-то строка""}},{{""S"",""Объект""},{""#"",4238019d-7e49-4fc9-91db-b6b951d5cf8e,{2,{{""S"",""Знач1""},{""L""}},{{""S"",""Знач2""},{""U""}}}}}}}",
                internalString);
        }
    }
}
