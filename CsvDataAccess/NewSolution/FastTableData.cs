using CsvDataAccess.Interface;

namespace CsvDataAccess.NewSolution
{
    public class FastTableData : ITableData
    {
        private readonly List<FastRow> _rows;

        public IEnumerable<string> Columns { get; }

        public int RowCount => _rows.Count;


        public FastTableData(IEnumerable<string> columns, List<FastRow> rows)
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
