using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OneSTools.BracketsFile;
using YY.YellowPlatformFormats.Core.Converters;

namespace YY.YellowPlatformFormats.Core.Types
{
    public class FixedStructure : BaseType, IEnumerable<KeyValuePair<string, object>>
    {
        public static BracketsNode ParseStructureNodeFromString(string sourceValue)
        {
            var nodes = BracketsParser.ParseBlock(sourceValue);
            if (nodes.Count == 3
                && nodes[0].Text == "#"
                && nodes[1].Text == TypeConverter.GetIdForType(typeof(FixedStructure)))
            {
                return nodes[2];
            }

            throw new Exception("Wrong string value for parsing");
        }

        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public FixedStructure()
        {
        }

        public FixedStructure(Structure sourceStructure)
        {
            foreach (var item in sourceStructure)
            {
                Insert(item.Key, item.Value);
            }
        }

        public FixedStructure(BracketsNode structureData)
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
                string valueDescription;
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
                    valueDescription = valueInfo[1];

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

                Insert(key.ToString(), value);
            }
        }

        public FixedStructure(string sourceValue) : this(ParseStructureNodeFromString(sourceValue))
        {
        }

        private void Insert(string key, object value)
        {
            _values.Add(key, value);
        }

        public int Count()
        {
            return _values.Count;
        }
        
        public bool Property(string key)
        {
            return _values.ContainsKey(key);
        }
        
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public object this[string index] => _values.GetValueOrDefault(index);

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
