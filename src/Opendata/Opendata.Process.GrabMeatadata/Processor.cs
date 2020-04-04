using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LiteDB;
using Opendata.Core;

namespace Opendata.Process.GrabMeatadata
{
    public class Processor : IDisposable
    {
        private readonly LiteDatabase _db;

        public Processor(string pathToDatabase)
        {
            _db = new LiteDatabase(pathToDatabase);
        }

        public async Task Run()
        {
            var inputs = _db.GetCollection<DatasetInput>().FindAll().ToList();
            var collection = _db.GetCollection<DatasetMetadata>();

            using var client = new HttpClient();
            var updated = new List<DatasetInput>();
            foreach (var input in inputs)
            {
                if (!string.IsNullOrEmpty(input.ErrorComment))
                {
                    continue;
                }

                if (input.IsProcessed)
                {
                    continue;
                }

                try
                {
                    var response = await client.GetAsync(input.MetaDataUrl);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();

                    var metadata = new DatasetMetadata
                    {
                        InputId = input.Id,
                        ErrorComment = string.Empty
                    };
                    metadata.ParseContent(content);

                    input.IsProcessed = string.IsNullOrEmpty(metadata.ErrorComment);
                    collection.Insert(metadata);
                }
                catch (Exception e)
                {
                    input.ErrorComment = e.Message;
                    input.IsProcessed = false;
                }
            }

            _db.GetCollection<DatasetInput>().Upsert(updated);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}