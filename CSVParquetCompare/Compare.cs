﻿using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace CSVParquetCompare
{
    public static class Compare
    {

        public static void CsvParquet(string location)
        {
            List<string> csvFilesList = new List<string>(Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Data\\" + location + "\\csv"));
            List<string> parquetFilesList = new List<string>(Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Data\\" + location + "\\parquet"));
            int nrOfEntries = 0;
            int failCount = 0;
            if (csvFilesList.Count != parquetFilesList.Count)
            {
                Console.WriteLine("Number of csv files (" + csvFilesList.Count + ") does not match the parquet files (" + parquetFilesList.Count + ")");
                Console.ReadKey();
                Console.Clear();
            }
            Console.Write("\nComparing files in " + location + "\n\n");
            for (int i = 0; (i < csvFilesList.Count); i++)
            {
                try
                {
                    var csvFile = DataReader.CSVDataReaderDomenius(csvFilesList[i]);
                    var parquetFile = DataReader.ParquetDataReader(parquetFilesList[i]);
                    CollectionAssert.AreEqual(csvFile, parquetFile);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("PASS ");
                    Console.ResetColor();
                    Console.WriteLine((csvFile.Count + " records - " + Path.GetFileName(csvFilesList[i])) + " & " + Path.GetFileName(parquetFilesList[i]));
                    nrOfEntries += csvFile.Count;
                }
                catch (Exception e)
                {
                    failCount++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("FAIL");
                    Console.ResetColor();
                    Console.WriteLine(" - " + Path.GetFileName(csvFilesList[i]) + " & " + Path.GetFileName(parquetFilesList[i]));
                    Console.WriteLine(e.Message.ToString());

                }
            }
            Console.ResetColor();
            Console.WriteLine("\nNumber of records checked for " + location + ": " + nrOfEntries.ToString() + " in " + csvFilesList.Count + " file pairs\n");
            Console.WriteLine("PASSED: " + (csvFilesList.Count - failCount).ToString());
            Console.WriteLine("FAILED: " + failCount.ToString());
            Console.ReadKey();

        }
    }

}