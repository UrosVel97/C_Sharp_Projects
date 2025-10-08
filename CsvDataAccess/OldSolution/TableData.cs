using CsvDataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataAccess.OldSolution
{
    public class TableData : ITableData
    {
        private readonly List<Row> _rows;

        public IEnumerable<string> Columns { get; }

        public int RowCount => _rows.Count;


        public TableData(IEnumerable<string> columns, List<Row> rows)
        {
            _rows = rows;
            Columns = columns;
        }

        public object GetVaule(string columnName, int rowIndex)
        {
            return _rows[rowIndex].GetAtColumn(columnName);
        }
    }
}
