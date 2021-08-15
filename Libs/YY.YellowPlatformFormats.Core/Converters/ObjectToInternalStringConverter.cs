using System;
using System.Globalization;
using System.Text;
using YY.YellowPlatformFormats.Core.Types;

namespace YY.YellowPlatformFormats.Core.Converters
{
    public static class ObjectToInternalStringConverter
    {
        public static string ValueToStringInternal<T>(T sourceValue)
        {
            string internalString;
            Type objectType = sourceValue.GetType();
            if (objectType == typeof(Structure))
            {
                var castValue = (Structure)(BaseType)(object)sourceValue;
                internalString = StructureToStringInternal(castValue);
            }
            else if (objectType == typeof(FixedStructure))
            {
                var castValue = (FixedStructure)(BaseType)(object)sourceValue;
                internalString = FixedStructureToStringInternal(castValue);
            }
            else if (objectType == typeof(Map))
            {
                var castValue = (Map)(object)sourceValue;
                internalString = MapToStringInternal(castValue);
            }
            else if (objectType == typeof(FixedMap))
            {
                var castValue = (FixedMap)(object)sourceValue;
                internalString = FixedMapToStringInternal(castValue);
            }
            else if (objectType == typeof(ObjectsArray))
            {
                var castValue = (ObjectsArray)(BaseType)(object)sourceValue;
                internalString = ObjectsArrayToStringInternal(castValue);
            }
            else if (objectType == typeof(FixedObjectsArray))
            {
                var castValue = (FixedObjectsArray)(BaseType)(object)sourceValue;
                internalString = FixedObjectsArrayToStringInternal(castValue);
            }
            else if (objectType == typeof(string))
            {
                var castValue = (string)(object)sourceValue;
                internalString = StringToStringInternal(castValue);
            }
            else if (objectType == typeof(decimal))
            {
                var castValue = (decimal)(object)sourceValue;
                internalString = DecimalToStringInternal(castValue);
            }
            else if (objectType == typeof(bool))
            {
                var castValue = (bool)(object)sourceValue;
                internalString = BoolToStringInternal(castValue);
            }
            else if (objectType == typeof(DateTime))
            {
                var castValue = (DateTime)(object)sourceValue;
                internalString = DateTimeToStringInternal(castValue);
            }
            else if (objectType == typeof(Guid))
            {
                var castValue = (Guid)(object)sourceValue;
                internalString = GuidToStringInternal(castValue);
            }
            else if (objectType == typeof(NullValue))
            {
                var castValue = (NullValue)(object)sourceValue;
                internalString = NullToStringInternal(castValue);
            }
            else if (objectType == typeof(UndefinedType))
            {
                var castValue = (UndefinedType)(object)sourceValue;
                internalString = UndefinedToStringInternal(castValue);
            }
            else if (objectType == typeof(ValueStorage))
            {
                var castValue = (ValueStorage)(object)sourceValue;
                internalString = ValueStorageToStringInternal(castValue);
            }
            else
            {
                internalString = NullToStringInternal();
            }

            return internalString;
        }

        private static string MapToStringInternal(Map sourceValue)
        {
            StringBuilder stringValue = new StringBuilder();
            string internalTypeId = TypeConverter.GetTypeIdByType(typeof(Map));

            var itemsCount = sourceValue.Count();
            stringValue.Append($"{{\"#\",{internalTypeId},{{{itemsCount}");
            if (itemsCount > 0)
            {
                stringValue.Append(",");
                int itemNumber = 0;
                foreach (var structureItem in sourceValue)
                {
                    itemNumber++;
                    stringValue.Append("{");
                    stringValue.Append(ValueToStringInternal(structureItem.Key));
                    stringValue.Append(",");
                    stringValue.Append(ValueToStringInternal(structureItem.Value));
                    stringValue.Append("}");

                    if (itemNumber != itemsCount) stringValue.Append(",");
                }
                stringValue.Append("}");
            }
            else
            {
                stringValue.Append("}");
            }

            stringValue.Append("}");

            return stringValue.ToString();
        }

        private static string FixedMapToStringInternal(FixedMap sourceValue)
        {
            StringBuilder stringValue = new StringBuilder();
            string internalTypeId = TypeConverter.GetTypeIdByType(typeof(FixedMap));

            var itemsCount = sourceValue.Count();
            stringValue.Append($"{{\"#\",{internalTypeId},{{{itemsCount}");
            if (itemsCount > 0)
            {
                stringValue.Append(",");
                int itemNumber = 0;
                foreach (var structureItem in sourceValue)
                {
                    itemNumber++;
                    stringValue.Append("{");
                    stringValue.Append(ValueToStringInternal(structureItem.Key));
                    stringValue.Append(",");
                    stringValue.Append(ValueToStringInternal(structureItem.Value));
                    stringValue.Append("}");

                    if (itemNumber != itemsCount) stringValue.Append(",");
                }
                stringValue.Append("}");
            }
            else
            {
                stringValue.Append("}");
            }

            stringValue.Append("}");

            return stringValue.ToString();
        }

        private static string StructureToStringInternal(Structure sourceValue)
        {
            StringBuilder stringValue = new StringBuilder();
            string internalTypeId = TypeConverter.GetTypeIdByType(typeof(Structure));

            var itemsCount = sourceValue.Count();
            stringValue.Append($"{{\"#\",{internalTypeId},{{{itemsCount}");
            if (itemsCount > 0)
            {
                stringValue.Append(",");
                int itemNumber = 0;
                foreach (var structureItem in sourceValue)
                {
                    itemNumber++;
                    stringValue.Append("{");
                    stringValue.Append(ValueToStringInternal(structureItem.Key));
                    stringValue.Append(",");
                    stringValue.Append(ValueToStringInternal(structureItem.Value));
                    stringValue.Append("}");

                    if (itemNumber != itemsCount) stringValue.Append(",");
                }
                stringValue.Append("}");
            }
            else
            {
                stringValue.Append("}");
            }

            stringValue.Append("}");

            return stringValue.ToString();
        }

        private static string FixedStructureToStringInternal(FixedStructure sourceValue)
        {
            StringBuilder stringValue = new StringBuilder();
            string internalTypeId = TypeConverter.GetTypeIdByType(typeof(FixedStructure));

            var itemsCount = sourceValue.Count();
            stringValue.Append($"{{\"#\",{internalTypeId},{{{itemsCount}");
            if (itemsCount > 0)
            {
                stringValue.Append(",");
                int itemNumber = 0;
                foreach (var structureItem in sourceValue)
                {
                    itemNumber++;
                    stringValue.Append("{");
                    stringValue.Append(ValueToStringInternal(structureItem.Key));
                    stringValue.Append(",");
                    stringValue.Append(ValueToStringInternal(structureItem.Value));
                    stringValue.Append("}");

                    if (itemNumber != itemsCount) stringValue.Append(",");
                }
                stringValue.Append("}");
            }
            else
            {
                stringValue.Append("}");
            }

            stringValue.Append("}");

            return stringValue.ToString();
        }

        private static string ObjectsArrayToStringInternal(ObjectsArray sourceValue)
        {
            StringBuilder stringValue = new StringBuilder();
            string internalTypeId = TypeConverter.GetTypeIdByType(typeof(ObjectsArray));

            var itemsCount = sourceValue.Count();
            stringValue.Append($"{{\"#\",{internalTypeId},{{{itemsCount}");
            if (itemsCount > 0)
            {
                stringValue.Append(",");
                int itemNumber = 0;
                foreach (var structureItem in sourceValue)
                {
                    itemNumber++;
                    stringValue.Append(ValueToStringInternal(structureItem));

                    if (itemNumber != itemsCount) stringValue.Append(",");
                }
                stringValue.Append("}");
            }
            else
            {
                stringValue.Append("}");
            }

            stringValue.Append("}");

            return stringValue.ToString();
        }

        private static string FixedObjectsArrayToStringInternal(FixedObjectsArray sourceValue)
        {
            StringBuilder stringValue = new StringBuilder();
            string internalTypeId = TypeConverter.GetTypeIdByType(typeof(FixedObjectsArray));

            var itemsCount = sourceValue.Count();
            stringValue.Append($"{{\"#\",{internalTypeId},{{{itemsCount}");
            if (itemsCount > 0)
            {
                stringValue.Append(",");
                int itemNumber = 0;
                foreach (var structureItem in sourceValue)
                {
                    itemNumber++;
                    stringValue.Append(ValueToStringInternal(structureItem));

                    if (itemNumber != itemsCount) stringValue.Append(",");
                }
                stringValue.Append("}");
            }
            else
            {
                stringValue.Append("}");
            }

            stringValue.Append("}");

            return stringValue.ToString();
        }

        private static string ValueStorageToStringInternal(ValueStorage sourceValue)
        {
            throw new NotImplementedException();
        }

        private static string GuidToStringInternal(Guid sourceValue)
        {
            var typeId = TypeConverter.GetTypeIdByType(typeof(Guid));
            return $"{{\"#\",{typeId},{sourceValue}}}";
        }

        private static string NullToStringInternal(NullValue sourceValue = null)
        {
            return "{\"L\"}";
        }

        private static string UndefinedToStringInternal(UndefinedType sourceValue)
        {
            return "{\"U\"}";
        }

        private static string DecimalToStringInternal(decimal sourceValue)
        {
            string formattedSting = sourceValue.ToString("0.00", CultureInfo.InvariantCulture);
            return $"{{\"N\",{formattedSting}}}";
        }

        private static string StringToStringInternal(string sourceValue)
        {
            return $"{{\"S\",\"{sourceValue}\"}}";
        }

        private static string BoolToStringInternal(bool sourceValue)
        {
            return $"{{\"B\",{(sourceValue ? 1 : 0)}}}";
        }

        private static string DateTimeToStringInternal(DateTime sourceValue)
        {
            return $"{{\"D\",{sourceValue.ToString("yyyMMddHHmmss")}}}";
        }
    }
}
