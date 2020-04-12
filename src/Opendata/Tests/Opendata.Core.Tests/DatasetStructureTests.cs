using Xunit;

namespace Opendata.Core.Tests
{
    public class DatasetStructureTests
    {
        [Fact]
        public void ParseContent()
        {
            var structure = new DatasetStructure();
            var content =
                "field name,english description,russian description,format\r\nDistrict,District name,Наименование района,string\r\nNumber1,The number of citizens registered for the purpose of providing land,\"Количество  граждан, поставленных  на учет  в целях  предоставления земельного участка\",integer\r\nNumber2,Number of people who received land,\"Количество граждан, получивших земельный участок\",string\r\nProvision,Provision of land plots (%),Обеспеченность земельными участками (%),float";
            structure.ParseContent(content);

            Assert.True(string.IsNullOrEmpty(structure.ErrorComment));
            Assert.Equal(4, structure.Fields.Count);
        }
    }
}