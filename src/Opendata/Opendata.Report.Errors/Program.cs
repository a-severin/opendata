using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using LiteDB;
using Opendata.Core;

namespace Opendata.Report.Errors
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Start report");
            var pathToDatabase = args[0];
            if (!File.Exists(pathToDatabase))
            {
                Console.WriteLine("Database does not founded");
                return;
            }

            using var db = new LiteDatabase(pathToDatabase);

            var lines = new List<string>();
            foreach (var data in db.GetCollection<DatasetData>()
                .Find(_ => _.ErrorComment != null && _.ErrorComment != string.Empty))
            {
                Console.WriteLine(data.ErrorComment);
                lines.Add(data.ErrorComment);
            }

            var tmp = Path.GetTempFileName();
            File.WriteAllLines(tmp, lines);
            Process.Start(
                @"C:\Users\severin.POINTCADNET\AppData\Local\Programs\Microsoft VS Code\Code.exe",
                tmp
            );
            Console.WriteLine("Finish report");
        }
    }
}