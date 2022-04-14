using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using Parquet;

namespace CSVParquetCompare
{
    public static class DataReader
    {
        public static List<string> CSVDataReader(string filename)
        {
            List<string> csvList = new List<string>();
            using (var rd = new StreamReader(filename))
            {
                while (!rd.EndOfStream)
                    csvList.Add(rd.ReadLine());
            }
            return csvList;
        }

        public static List<string> CSVDataReaderDomenius(string filename)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ","

            };
            List<string> csvList = new List<string>();
            using (var fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var fileReader = new StreamReader(filename, Encoding.UTF8))
                using (var csvResult = new CsvReader(fileReader, configuration))
                {

                    {
                        while (!fileReader.EndOfStream)
                            csvList.Add(fileReader.ReadLine());
                    }
                    return csvList;
                }
            }
        }

        public static List<string> CSVDataReaderDomenius2(string filename)
        {
            List<string> csvList = new List<string>();
            MemoryStream csvData = new MemoryStream();
            using (FileStream source = File.Open(filename, FileMode.Open))
                source.CopyTo(csvData);
            using (var rd = new StreamReader(filename))
            {
                while (!rd.EndOfStream)
                    csvList.Add(rd.ReadLine());
            }
            return csvList;
        }

        public static List<string> ParquetDataReader(string filename)
        {

            List<string> parquetList = new List<string>();
            MemoryStream ParquetData = new MemoryStream();
            using (FileStream source = File.Open(filename, FileMode.Open))
                source.CopyTo(ParquetData);
            using (ParquetReader pr = new ParquetReader(ParquetData))
            {
                var table = pr.ReadAsTable();
                foreach (var row in table)
                {
                    ParquetResponse response = JsonConvert.DeserializeObject<ParquetResponse>(row.ToString());
                    parquetList.Add(response.Domain);
                }
            }
            return parquetList;
        }
    }

    public class ParquetResponse
    {
        public string Domain { get; set; }
    }
}
