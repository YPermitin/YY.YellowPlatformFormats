using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OneSTools.BracketsFile;
using YY.YellowPlatformFormats.Core.Converters;

namespace YY.YellowPlatformFormats.Core.Types
{
    public class FixedMap : BaseType, IEnumerable<KeyValuePair<object, object>>
    {
        public static BracketsNode ParseStructureNodeFromString(string sourceValue)
        {
            var nodes = BracketsParser.ParseBlock(sourceValue);
            if (nodes.Count == 3
                && nodes[0].Text == "#"
                && nodes[1].Text == TypeConverter.GetIdForType(typeof(FixedMap)))
            {
                return nodes[2];
            }
            else
            {
                throw new Exception("Wrong string value for parsing");
            }
        }

        private readonly Dictionary<object, object> _values = new Dictionary<object, object>();

        public FixedMap()
        {
        }

        public FixedMap(Map sourceMap)
        {
            foreach (var item in sourceMap)
            {
                Insert(item.Key, item.Value);
            }
        }

        public FixedMap(BracketsNode structureData)
        {
            var itemsCount = int.Parse(structureData[0].Text);
            for (int i = 0; i < itemsCount; i++)
            {
                var item = structureData[i + 1];

                // Ключ
                var keyInfo = item.Nodes[0];
                string keyTypeDescription = keyInfo[0];
                Type keyType = TypeConverter.GetTypeById(keyTypeDescription);
                string keyValueDescription = keyInfo[1];
                object key = TypeConverter.ConvertTo(keyValueDescription, keyType);

                // Значение
                var valueInfo = item.Nodes[1];
                string valueTypeDescription;

                Type valueType;
                object value;

                if (valueInfo.Count == 1)
                {
                    valueTypeDescription = valueInfo[0];

                    valueType = TypeConverter.GetTypeById(valueTypeDescription);
                    value = TypeConverter.ConvertTo(null, valueType);
                }
                else if (valueInfo.Count == 2)
                {
                    valueTypeDescription = valueInfo[0];
                    string valueDescription = valueInfo[1];

                    valueType = TypeConverter.GetTypeById(valueTypeDescription);
                    value = TypeConverter.ConvertTo(valueDescription, valueType);
                }
                else if (valueInfo.Count == 3)
                {
                    valueTypeDescription = valueInfo[1];
                    valueType = TypeConverter.GetTypeById(valueTypeDescription);
                    value = TypeConverter.ConvertTo(valueInfo[2], valueType);
                }
                else
                    throw new Exception("Info about type not found");

                Insert(key, value);
            }
        }

        public FixedMap(string sourceValue) : this(ParseStructureNodeFromString(sourceValue))
        {
        }

        private void Insert(object key, object value)
        {
            _values.Add(key, value);
        }

        public int Count()
        {
            return _values.Count;
        }

        public bool Property(object key)
        {
            return _values.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public object this[object index] => _values.GetValueOrDefault(index);

        public override string ToString()
        {
            StringBuilder presentation = new StringBuilder();

            foreach (var structureItem in this)
            {
                presentation.AppendLine($"[{structureItem.Key}]: {structureItem.Value}");
            }

            return presentation.ToString().Trim();
        }
    }
}
