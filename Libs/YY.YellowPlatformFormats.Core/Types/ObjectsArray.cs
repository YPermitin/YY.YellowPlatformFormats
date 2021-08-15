﻿using OneSTools.BracketsFile;
using System;
using System.Collections;
using System.Collections.Generic;
using YY.YellowPlatformFormats.Core.Converters;

namespace YY.YellowPlatformFormats.Core.Types
{
    public class ObjectsArray : BaseType, IEnumerable<object>
    {
        public static BracketsNode ParseObjectsArrayNodeFromString(string sourceValue)
        {
            var nodes = BracketsParser.ParseBlock(sourceValue);
            if (nodes.Count == 3
                && nodes[0].Text == "#"
                && nodes[1].Text == TypeConverter.GetIdForType(typeof(ObjectsArray)))
            {
                return nodes[2];
            }
            else
            {
                throw new Exception("Wrong string value for parsing");
            }
        }

        private readonly List<object> _values = new List<object>();

        public ObjectsArray()
        {            
        }

        public ObjectsArray(string sourceValue) : this(ParseObjectsArrayNodeFromString(sourceValue))
        {
        }

        public ObjectsArray(BracketsNode structureData)
        {
            var itemsCount = int.Parse(structureData[0].Text);
            for (int i = 0; i < itemsCount; i++)
            {
                var valueInfo = structureData[i + 1];

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

                Add(value);
            }
        }
        
        public void Add(object value)
        {
            _values.Add(value);
        }

        public void Remove(int index)
        {
            _values.RemoveAt(index);
        }

        public int Find(object value)
        {
            return _values.IndexOf(value);
        }

        public int Count()
        {
            return _values.Count;
        }

        public IEnumerator<object> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }
}
