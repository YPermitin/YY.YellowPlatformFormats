using System;
using System.Collections;
using System.Collections.Generic;
using YY.YellowPlatformFormats.Core.Types;

namespace YY.YellowPlatformFormats.Core.Tests.Converters
{
    public class TypeConverterConvertToTestsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "L", typeof(NullValue) };
            yield return new object[] { "U", typeof(UndefinedType) };
            yield return new object[] { "123", typeof(decimal) };
            yield return new object[] { "From .NET with love!", typeof(string) };
            yield return new object[] { "1", typeof(bool) };
            yield return new object[] { "20210801101010", typeof(DateTime) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
