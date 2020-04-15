using System.Collections.Generic;
using Opendata.Web.Client.Model;

namespace Opendata.Web.Client.Repository
{
    public interface IDataRepository
    {
        IEnumerable<Region> GetRegions();
    }
}