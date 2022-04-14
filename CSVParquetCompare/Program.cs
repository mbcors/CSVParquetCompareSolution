using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CSVParquetCompare
{
    class Program
    {/// <summary>
    /// Move data.zip to \Data folder
    /// Change the location parameter as required (have csv/parquet structure inside)
    /// </summary>
    /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Compare.CsvParquet("Zonefiles");
            //Compare.CsvParquet("WhoisXML");
            Compare.CsvParquet("Domenius");
        }
    }
}
