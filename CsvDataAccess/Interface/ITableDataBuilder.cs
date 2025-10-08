using CsvDataAccess.CsvReading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataAccess.Interface
{
    public interface ITableDataBuilder
    {
        ITableData Build(CsvData csvData);

    }
}
