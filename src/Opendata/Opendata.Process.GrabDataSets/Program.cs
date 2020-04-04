using System;
using System.Text;
using System.Threading.Tasks;

namespace Opendata.Process.GrabDataSets
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Start processing");
            using var processor = new Processor(@"C:\GitHub\opendata\tmp\opendata.db", args[0], args[1], args[2]);
            await processor.Run();
            Console.WriteLine("Finish processing");
        }
    }
}