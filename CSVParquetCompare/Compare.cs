using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Parquet;

namespace CSVParquetCompare
{
    public static class Compare
    {

        public static void CsvParquet()
        {
            List<string> csvFilesList = new List<string>(Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Data\\csv_parquet\\csv"));
            List<string> parquetFilesList = new List<string>(Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Data\\csv_parquet\\parquet"));
            if (csvFilesList.Count != parquetFilesList.Count)
            {
                Console.WriteLine("Number of csv files (" + csvFilesList.Count + ") does not match the parquet files (" + parquetFilesList.Count + ")");
                Console.ReadKey();
                Console.Clear();
            }
            for (int i = 0; (i < csvFilesList.Count); i++)
            {
                try
                {
                    var csvFile = DataReader.CSVDataReader(csvFilesList[i]);
                    var parquetFile = DataReader.ParquetDataReader(parquetFilesList[i]);
                    CollectionAssert.AreEqual(csvFile, parquetFile);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("PASS");
                    Console.ResetColor();
                    Console.WriteLine((" - " + Path.GetFileName(csvFilesList[i])) + " & " + Path.GetFileName(parquetFilesList[i]));
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("FAIL");
                    Console.ResetColor();
                    Console.WriteLine(" - " + Path.GetFileName(csvFilesList[i]) + " & " + Path.GetFileName(parquetFilesList[i]));
                    Console.WriteLine(e.Message.ToString());

                }
            }
            Console.ResetColor();
            Console.ReadKey();
        }
    }

}