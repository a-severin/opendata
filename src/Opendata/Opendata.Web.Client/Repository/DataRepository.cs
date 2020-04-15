using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Opendata.Core;
using Opendata.Web.Client.Model;

namespace Opendata.Web.Client.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly LiteDatabase _db;

        public DataRepository()
        {
            _db = new LiteDatabase(@"C:\GitHub\opendata\tmp\34_opendata.db");
        }

        public IEnumerable<Region> GetRegions()
        {
            var collection = _db.GetCollection<DatasetInput>();
            var set = new SortedSet<Region>(new Region.Comparer());
            foreach (var datasetInput in collection.Query().OrderBy(_ => _.RegionCode).ToEnumerable())
            {
                set.Add(new Region(datasetInput.RegionCode, datasetInput.RegionName));
            }

            return set;
        }
    }
}