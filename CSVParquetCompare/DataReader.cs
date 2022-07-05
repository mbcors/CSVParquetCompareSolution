using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
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

        public static List<string> CSVDataReaderIcann(string filename)
        {
            List<string> uniqueList = new List<string>();
            List<string> result = new List<string>();
            DataTable datatable = new DataTable();
            StreamReader streamreader = new StreamReader(@filename);
            char[] delimiter = new char[] { '\t' };
            //string[] columnheaders = streamreader.ReadLine().Split(delimiter);
            string[] columnheaders = new string[5];
            columnheaders[0] = "c0";
            columnheaders[1] = "c1";
            columnheaders[2] = "c2";
            columnheaders[3] = "c3";
            columnheaders[4] = "c4";
            foreach (string columnheader in columnheaders)
            {
                datatable.Columns.Add(columnheader); // I've added the column headers here.
            }

            while (streamreader.Peek() > 0)
            {
                DataRow datarow = datatable.NewRow();
                datarow.ItemArray = streamreader.ReadLine().Split(delimiter);
                datatable.Rows.Add(datarow);
            }

            foreach (DataRow row in datatable.Rows)
            {
                foreach (DataColumn column in datatable.Columns)
                {
                    //check what columns you need
                    if (column.ColumnName == "c0" )
                    {
                        result.Add(row[column].ToString());
                    }
                }
              
                uniqueList = result.Distinct().ToList();
            }
            return uniqueList;
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
