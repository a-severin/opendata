using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LiteDB;
using Opendata.Core;

namespace Opendata.Process.GrabData
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
            var structureCollection = _db
                .GetCollection<DatasetStructure>()
                .FindAll()
                .ToList();
            using var client = new HttpClient();
            var updated = new List<DatasetStructure>();
            foreach (var structure in structureCollection)
            {
                if (!string.IsNullOrEmpty(structure.ErrorComment))
                {
                    continue;
                }

                if (structure.IsProcessed)
                {
                    continue;
                }

                try
                {
                    var metadata = _db
                        .GetCollection<DatasetMetadata>()
                        .FindById(structure.MetadataId);

                    var response = await client.GetAsync(metadata.DataUrl);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();

                    var data = new DatasetData
                    {
                        StructureId = structure.Id,
                        ErrorComment = string.Empty
                    };
                    data.ParseContent(content, structure.Fields);

                    structure.IsProcessed = string.IsNullOrEmpty(data.ErrorComment);
                    _db.GetCollection<DatasetData>().Insert(data);
                }
                catch (Exception e)
                {
                    structure.IsProcessed = false;
                    structure.ErrorComment = e.Message;
                }

                updated.Add(structure);
            }

            _db
                .GetCollection<DatasetStructure>()
                .Upsert(updated);
        }
    }
}