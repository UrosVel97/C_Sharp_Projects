using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataAccess.CsvReading
{
    public class CsvData
    {
        public string[] Columns { get; }

        public IEnumerable<string[]> Rows { get; }  

        public CsvData(string[] columns, IEnumerable<string[]> rows)
        {
            Columns = columns;
            Rows = rows;
        }
    }
}
