using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDataAccess.PerformanceTesting
{
    public class TestResult
    {
        public long MemoryIncreaseInBytes { get; }

        public TimeSpan TimeOfBuildingTable { get; }

        public TimeSpan TimeOfDataReading { get; }

        public TestResult(long memoryIncreaseInBytes, TimeSpan timeOfLoading, TimeSpan timeOfDataReading)
        {
            MemoryIncreaseInBytes = memoryIncreaseInBytes;
            TimeOfBuildingTable = timeOfLoading;
            TimeOfDataReading = timeOfDataReading;
        }

    }
}
