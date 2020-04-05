using System;
using System.Net.Http;
using System.Threading.Tasks;
using LiteDB;
using Opendata.Core;

namespace Opendata.Process.GrabDataSets
{
    public class Processor : IDisposable
    {
        private readonly string _regionCode;
        private readonly string _regionName;
        private readonly string _url;
        private readonly LiteDatabase _db;

        public Processor(string pathToDatabase, string regionCode, string regionName, string url)
        {
            _regionCode = regionCode;
            _regionName = regionName;
            _url = url;
            _db = new LiteDatabase(pathToDatabase);
        }

        public async Task Run()
        {
            using var client = new HttpClient();

            var response = await client.GetAsync(_url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Run(content);
        }

        public void Run(string content)
        {
            var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var collection = _db.GetCollection<DatasetInput>();
            foreach (var line in lines)
            {
                if (!line.Contains("http"))
                {
                    continue;
                }

                if (line.Contains("http://opendata.gosmonitor.ru/standard/3.0"))
                {
                    continue;
                }

                var input = new DatasetInput
                {
                    RegionCode = _regionCode,
                    RegionName = _regionName,
                    RawData = line,
                    IsProcessed = false,
                    ErrorComment = string.Empty
                };
                
                input.ParseLine(line);

                collection.Insert(input);
            }
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}