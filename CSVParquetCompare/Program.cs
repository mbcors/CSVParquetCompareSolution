using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSVParquetCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> csvFilesList = new List<string>(Directory.GetFiles(Directory.GetCurrentDirectory() + "\\csv"));
            List<string> parquetFilesList = new List<string>(Directory.GetFiles(Directory.GetCurrentDirectory() + "\\parquet"));
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
                    var csvFile = Comparer.CSVDataReader(csvFilesList[i]);
                    var parquetFile = Comparer.ParquetDataReader(parquetFilesList[i]);
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
