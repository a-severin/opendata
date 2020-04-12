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

        public void Dispose()
        {
            _db?.Dispose();
        }

        public async Task Run()
        {
            var inputs = _db.GetCollection<DatasetInput>().FindAll().ToList();

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
                    _db.GetCollection<DatasetMetadata>().Insert(metadata);
                }
                catch (Exception e)
                {
                    input.ErrorComment = e.Message;
                    input.IsProcessed = false;
                }

                updated.Add(input);
            }

            _db.GetCollection<DatasetInput>().Upsert(updated);
        }
    }
}