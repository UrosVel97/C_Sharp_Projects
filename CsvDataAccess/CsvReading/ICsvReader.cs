using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataAccess.CsvReading
{
    public interface ICsvReader
    {
        CsvData Read(string filePath);
    }
}
