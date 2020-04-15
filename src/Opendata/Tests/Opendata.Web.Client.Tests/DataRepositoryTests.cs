using System;
using System.Linq;
using Opendata.Web.Client.Repository;
using Xunit;

namespace Opendata.Web.Client.Tests
{
    public class DataRepositoryTests
    {
        [Fact]
        public void GetRegions()
        {
            var dataRepository = new DataRepository();
            var enumerable = dataRepository.GetRegions();
            Assert.Contains(enumerable, _ => _.Code == "34");
        }
    }
}