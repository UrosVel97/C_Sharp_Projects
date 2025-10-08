using CsvDataAccess.CsvReading;
using CsvDataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataAccess.OldSolution
{
    public class TableDataBuilder : ITableDataBuilder
    {

        public ITableData Build(CsvData csvData)
        {
            var resultRows = new List<Row>();

            foreach (var row in csvData.Rows)
            {
                var newRowData=new Dictionary<string, object>();

                for (int columnIndex = 0; columnIndex < csvData.Columns.Length; columnIndex++)
                {
                    var column= csvData.Columns[columnIndex];
                    string valueAsString = row[columnIndex];
                    newRowData[column] = ConvertValueToTargetType(valueAsString);
                }

                resultRows.Add(new Row(newRowData));
            }

            return new TableData(csvData.Columns, resultRows);
        }

        private object ConvertValueToTargetType(string valueAsString)
        {
            if(string.IsNullOrEmpty(valueAsString))
            {
                return null;
            }

            if(valueAsString=="TRUE")
            {
                return true;

            }
            if(valueAsString=="FALSE")
            {
                return false;
            }
            if(valueAsString.Contains(".") && decimal.TryParse(valueAsString,out var valueAsDecimal))
            {
                return valueAsDecimal;
            }
            if(int.TryParse(valueAsString,out var valueAsInt))
            {
                return valueAsInt;
            }

            return valueAsString;
        }
    }
}
