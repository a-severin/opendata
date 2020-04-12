using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Opendata.Process.GrabData
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Start processing");
            var pathToDatabase = args[0];
            if (!File.Exists(pathToDatabase))
            {
                Console.WriteLine("Database does not founded");
                return;
            }

            using var processor = new Processor(pathToDatabase);
            await processor.Run();
            Console.WriteLine("Finish processing");
        }
    }
}