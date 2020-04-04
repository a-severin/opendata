using System;
using System.Text;
using System.Threading.Tasks;

namespace Opendata.Process.GrabMeatadata
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Start processing");
            using var processor = new Processor(@"C:\GitHub\opendata\tmp\opendata.db");
            await processor.Run();
            Console.WriteLine("Finish processing");
        }
    }
}
