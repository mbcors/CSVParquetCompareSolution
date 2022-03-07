using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    var response = JsonConvert.DeserializeObject<ParquetResponse>(row.ToString());
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
