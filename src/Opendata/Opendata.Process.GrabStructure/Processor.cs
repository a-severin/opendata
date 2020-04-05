using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LiteDB;
using Opendata.Core;

namespace Opendata.Process.GrabStructure
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
            var metadataCollection = _db.GetCollection<DatasetMetadata>().FindAll().ToList();
            using var client = new HttpClient();
            var updated = new List<DatasetMetadata>();
            foreach (var metadata in metadataCollection)
            {
                if (!string.IsNullOrEmpty(metadata.ErrorComment))
                {
                    continue;
                }

                if (metadata.IsProcessed)
                {
                    continue;
                }

                try
                {
                    var response = await client.GetAsync(metadata.StructureUrl);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();

                    var structure = new DatasetStructure
                    {
                        MetadataId = metadata.Id,
                        ErrorComment = string.Empty
                    };
                    structure.ParseContent(content);

                    metadata.IsProcessed = string.IsNullOrEmpty(structure.ErrorComment);
                    _db.GetCollection<DatasetStructure>().Insert(structure);
                }
                catch (Exception e)
                {
                    metadata.IsProcessed = false;
                    metadata.ErrorComment = e.Message;
                }

                updated.Add(metadata);
            }

            _db.GetCollection<DatasetMetadata>().Upsert(updated);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}