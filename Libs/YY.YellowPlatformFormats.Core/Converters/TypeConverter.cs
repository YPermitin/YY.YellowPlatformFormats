using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OneSTools.BracketsFile;
using YY.YellowPlatformFormats.Core.Types;

namespace YY.YellowPlatformFormats.Core.Converters
{
    public static class TypeConverter
    {
        private static readonly Dictionary<string, Type> _typeMapping = new Dictionary<string, Type>();
        private static readonly Dictionary<Type, string> _typeIdMapping = new Dictionary<Type, string>();

        static TypeConverter()
        {
            _typeMapping.Add("L", typeof(NullValue));         // Null
            _typeMapping.Add("U", typeof(UndefinedType));     // Неопределено
            _typeMapping.Add("N", typeof(decimal));           // Число ({\"N\",<?>}): {"N", 3.14} или {"N", 777}
            _typeMapping.Add("S", typeof(string));            // Строка ({"S","<?>"}): {"S", "тест"} = тест или {"S", " ""} "} = "}
            _typeMapping.Add("B", typeof(bool));              // Булево ({"B",<?>}): {"B", 0}, {"B", 1}
            _typeMapping.Add("D", typeof(DateTime));          // Дата ({"D",<?>}), {"D",00010101000000}  = пустая дата или {"D",20190830203450}  = 20:34:50 30 августа 2019 года
            _typeMapping.Add(                                 // Структура
                "4238019d-7e49-4fc9-91db-b6b951d5cf8e",
                typeof(Structure));
            _typeMapping.Add(                                 // Соответствие
                "3d48feae-a9c6-4c5a-a099-9eb6477630c6",
                typeof(Map));
            _typeMapping.Add(                                 // Хранилище значений
                "e199ca70-93cf-46ce-a54b-6edc88c3a296",
                typeof(ValueStorage));
            _typeMapping.Add(                                 // GUID
                "fc01b5df-97fe-449b-83d4-218a090e681e",
                typeof(Guid));
            _typeMapping.Add(                                 // Таблица значений
                "acf6192e-81ca-46ef-93a6-5a6968b78663",
                typeof(ValueTable));
            _typeMapping.Add(                                 // ФиксированнаяСтруктура
                "3ee983d7-ace7-40f9-bb7e-2e916fcddd56",
                typeof(FixedStructure));
            _typeMapping.Add(                                 // ФиксированноеСоответствие
                "220455ea-6c85-4513-996f-bbe79ed07774",
                typeof(FixedMap));
            _typeMapping.Add(                                 // Массив
                "51e7a0d2-530b-11d4-b98a-008048da3034",
                typeof(ObjectsArray));
            _typeMapping.Add(                                 // Фиксированный массив
                "4500381b-db30-4a10-9db4-990038032acf",
                typeof(FixedObjectsArray));

            foreach (var typeMappingItem in _typeMapping)
            {
                _typeIdMapping.Add(typeMappingItem.Value, typeMappingItem.Key);
            }
        }

        public static Type GetTypeById(string typeId)
        {
            if (_typeMapping.TryGetValue(typeId, out Type foundType))
                return foundType;

            return typeof(NullValue);
        }

        public static string GetTypeIdByType(Type type)
        {
            if (_typeIdMapping.TryGetValue(type, out string foundTypeId))
                return foundTypeId;

            return _typeIdMapping.GetValueOrDefault(typeof(NullValue));
        }

        public static string GetIdForType(Type typeInfo)
        {
            return _typeMapping
                .Where(m => m.Value == typeInfo)
                .Select(m => m.Key)
                .FirstOrDefault();
        }

        public static object ConvertTo(object sourceValue, Type destinationType)
        {
            object newValue;

            if (destinationType == typeof(NullValue))
            {
                newValue = NullValue.Default;
            }
            else if (destinationType == typeof(UndefinedType))
            {
                newValue = UndefinedType.Default;
            }
            else if (destinationType == typeof(string))
            {
                newValue = sourceValue;
            }
            else if (destinationType == typeof(decimal))
            {
                newValue = decimal.Parse(sourceValue.ToString().Replace('.', ','));
            }
            else if (destinationType == typeof(bool))
            {
                newValue = sourceValue.ToString() == "1";
            }
            else if (destinationType == typeof(DateTime))
            {
                newValue = DateTime.ParseExact(sourceValue.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            }
            else if (destinationType == typeof(Structure))
            {
                newValue = new Structure((BracketsNode)sourceValue);
            }
            else if (destinationType == typeof(FixedStructure))
            {
                newValue = new FixedStructure((BracketsNode)sourceValue);
            }
            else if (destinationType == typeof(Map))
            {
                newValue = new Map((BracketsNode)sourceValue);
            }
            else if (destinationType == typeof(FixedMap))
            {
                newValue = new FixedMap((BracketsNode)sourceValue);
            }
            else if (destinationType == typeof(ObjectsArray))
            {
                newValue = new ObjectsArray((BracketsNode)sourceValue);
            }
            else if (destinationType == typeof(FixedObjectsArray))
            {
                newValue = new FixedObjectsArray((BracketsNode)sourceValue);
            }
            else if (destinationType == typeof(ValueStorage))
            {
                if (sourceValue is BracketsNode)
                {
                    newValue = new ValueStorage((BracketsNode)sourceValue);
                }
                else
                {
                    // TODO Реализовать преобразование из строки внутреннего формата
                    newValue = NullValue.Default;
                }
            }
            else if (destinationType == typeof(ValueTable))
            {
                if (sourceValue is BracketsNode)
                {
                    newValue = new ValueTable((BracketsNode)sourceValue);
                }
                else
                {
                    // TODO Реализовать преобразование из строки внутреннего формата
                    newValue = NullValue.Default;
                }
            }
            else if (destinationType == typeof(Guid))
            {
                newValue = Guid.Parse(sourceValue.ToString());
            }
            else
            {
                throw new InvalidCastException($"Unknown type for converting: {destinationType.FullName}");
            }

            return newValue;
        }
    }
}
