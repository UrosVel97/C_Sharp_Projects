using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataAccess.CsvReading
{
    public class CsvReader : ICsvReader
    {

        public CsvData Read(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                var columns = reader.ReadLine().Split(",");

                var rows = new List<string[]>();

                while (!reader.EndOfStream)
                {
                    var cellsInRow = reader.ReadLine().Split(",");
                    rows.Add(cellsInRow);
                }

                return new CsvData(columns, rows);
            }
        }
    }
}
