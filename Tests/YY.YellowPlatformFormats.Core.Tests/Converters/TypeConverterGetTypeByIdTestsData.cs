using System;
using System.Collections;
using System.Collections.Generic;
using YY.YellowPlatformFormats.Core.Types;

namespace YY.YellowPlatformFormats.Core.Tests.Converters
{
    public class TypeConverterGetTypeByIdTestsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { NullValue.Default };
            yield return new object[] { UndefinedType.Default };
            yield return new object[] { 12345.67890M };
            yield return new object[] { "From .NET with love!" };
            yield return new object[] { true };
            yield return new object[] { DateTime.Now };
            yield return new object[] { new ObjectsArray() };
            yield return new object[] { new FixedObjectsArray() };
            yield return new object[] { new Map() };
            yield return new object[] { new FixedMap() };
            yield return new object[] { new Structure() };
            yield return new object[] { new FixedStructure() };
            yield return new object[] { new ValueTable() };
            yield return new object[] { ValueStorage.FromValue(NullValue.Default) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
