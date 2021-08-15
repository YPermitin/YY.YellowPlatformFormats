using OneSTools.BracketsFile;
using System;
using System.Collections.Generic;
using System.Linq;
using YY.YellowPlatformFormats.Core.Converters;

namespace YY.YellowPlatformFormats.Core.Types
{
    public class ValueTable
    {
        private List<ColumnDefination> _columns;
        private Dictionary<int, ColumnDefination> _columnsMappingInRows;
        private Dictionary<int, ColumnDefination> _columnsMappingInDefination;
        private List<ValueTableRow> _rows;

        public IReadOnlyList<ColumnDefination> Columns
        {
            get
            {
                return _columns.AsReadOnly();
            }
        }

        public IReadOnlyList<ValueTableRow> Rows
        {
            get
            {
                return _rows.AsReadOnly();
            }
        }

        public ValueTable()
        {
            _columns = new List<ColumnDefination>();
            _columnsMappingInRows = new Dictionary<int, ColumnDefination>();
            _columnsMappingInDefination = new Dictionary<int, ColumnDefination>();
            _rows = new List<ValueTableRow>();
        }

        public ValueTable(BracketsNode sourceValue) : this()
        {
            var columnsInfo = sourceValue[1];
            var columnsCount = int.Parse(columnsInfo[0].Text);
            for (int i = 0; i < columnsCount; i++)
            {
                var columnItemInfo = columnsInfo[i+1];
                int columnNumber = int.Parse(columnItemInfo[0]);
                string columnName = columnItemInfo[1].Text;

                var columnsTypesInfo = columnItemInfo[2]
                    .Nodes.Where(ie => ie.Text != "Pattern")
                    .ToList();
                List<ColumnTypeDefination> columnTypes = new List<ColumnTypeDefination>();
                foreach (var columnTypesInfo in columnsTypesInfo)
                {
                    Type columnType = TypeConverter.GetTypeById(columnTypesInfo[0].Text);
                    // TODO: Реализовать чтение доп. информации в описании типа
                    columnTypes.Add(new ColumnTypeDefination(columnType));
                }
                string columnPresentation = columnItemInfo[3].Text;
                int columnWeight = int.Parse(columnItemInfo[4].Text);

                ColumnDefination columnInfo = new ColumnDefination(columnNumber, columnName, columnTypes, columnPresentation, columnWeight);
                _columns.Add(columnInfo);
            }

            var rowsInfo = sourceValue[2];
            int internalRowsToUse = int.Parse(rowsInfo[1].Text);
            // Маппинг колонок
            int lastIndex = 1;
            for (int i = 0; i < internalRowsToUse; i++)
            {
                int columnIndexInRows = 2 + (2 * i);
                int columnIndexInDefinition = columnIndexInRows + 1;

                int indexInRows = int.Parse(rowsInfo[columnIndexInRows].Text);
                int indexInDefinition = int.Parse(rowsInfo[columnIndexInDefinition].Text);
                ColumnDefination columnDefinition = _columns.First(c => c.Number == indexInDefinition);
                _columnsMappingInDefination.Add(indexInDefinition, columnDefinition);
                _columnsMappingInRows.Add(indexInRows, columnDefinition);

                lastIndex = columnIndexInDefinition;
            }

            var rowsData = rowsInfo[lastIndex + 1];
            int rowsCount = int.Parse(rowsData[1].Text);
            for (int i = 0; i < rowsCount; i++)
            {
                var rowInfo = rowsData[i + 2];
                int valuesInRowCount = int.Parse(rowInfo[2].Text);

                ValueTableRow row = new ValueTableRow();
                for (int ri = 0; ri < valuesInRowCount; ri++)
                {
                    var valueInColumnInfo = rowInfo[ri + 3];
                    Type valueInRowType = TypeConverter.GetTypeById(valueInColumnInfo[0].Text);

                    _columnsMappingInRows.TryGetValue(ri, out ColumnDefination columnDefinition);

                    object valueConverted;
                    if (valueInColumnInfo.Count > 1)
                    {
                        string valueAsString = valueInColumnInfo[1].Text;
                        valueConverted = TypeConverter.ConvertTo(valueAsString, valueInRowType);
                    } else
                    {
                        valueConverted = TypeConverter.ConvertTo(string.Empty, valueInRowType);
                    }

                    if(columnDefinition != null)
                    {
                        row.AddValue(columnDefinition, valueConverted);
                    }
                }
                _rows.Add(row);
            }
        }

        public int Count()
        {
            return _rows.Count;
        }

        public override string ToString()
        {
            return $"Таблица значений ({_rows.Count})";
        }

        public class ColumnDefination
        {
            public int Number { get; }
            public string Name { get; }
            public List<ColumnTypeDefination> Types { get; }
            public string Presentation { get; }
            public int Weight { get; }


            public ColumnDefination(int number, string name, List<ColumnTypeDefination> types, string presentation, int weight)
            {
                Number = number;
                Name = name;
                Types = types;
                Presentation = presentation;
                Weight = weight;
            }

            public override string ToString()
            {
                return $"{Name} ({Presentation})";
            }
        }

        public class ColumnTypeDefination
        {
            public Type ColumnType { get; }
            public int MaxLength { get; }

            public ColumnTypeDefination()
            {
                MaxLength = 0;
            }

            public ColumnTypeDefination(Type columnType, int maxLength = 0)
            {
                ColumnType = columnType;
                MaxLength = maxLength;
            }

            public override string ToString()
            {
                return ColumnType.FullName;
            }
        }

        public class ValueTableRow
        {
            private Dictionary<ColumnDefination, object> _values = new Dictionary<ColumnDefination, object>();

            public IReadOnlyDictionary<ColumnDefination, object> Values
            {
                get
                {
                    return _values;
                }
            }

            public object this[ColumnDefination column]
            {
                get
                {
                    return _values[column];
                }
                set
                {
                    _values[column] = value;
                }
            }

            public void AddValue(ColumnDefination column, object value)
            {
                _values[column] = value;
            }

            public object GetValue(ColumnDefination column)
            {
                if (_values.TryGetValue(column, out object value))
                    return value;

                return null;
            }

            public void RemoveValue(ColumnDefination column)
            {
                if (_values.ContainsKey(column))
                    _values.Remove(column);
            }

            public override string ToString()
            {
                return String.Join(", ", _values.Select(e => e.Value).ToArray());
            }
        }        
    }
}
