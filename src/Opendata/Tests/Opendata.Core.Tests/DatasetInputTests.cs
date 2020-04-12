using Xunit;

namespace Opendata.Core.Tests
{
    public class DatasetInputTests
    {
        [Fact]
        public void ParseLine()
        {
            var input = new DatasetInput();
            var line =
                "3444200430-LabourWage,Региональный МРОТ,http://opendata.volganet.ru/3444200430-LabourWage/meta.csv,CSV;";
            input.ParseLine(line);

            Assert.Equal("3444200430-LabourWage", input.SubsetCode);
            Assert.Equal("Региональный МРОТ", input.SubsetName);
            Assert.Equal("http://opendata.volganet.ru/3444200430-LabourWage/meta.csv", input.MetaDataUrl);
        }
    }
}