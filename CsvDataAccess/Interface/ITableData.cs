using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataAccess.Interface
{
    public interface ITableData
    {
        IEnumerable<string> Columns { get; }
        int RowCount { get; }
        object GetVaule(string columnName, int rowIndex);

    }
}
