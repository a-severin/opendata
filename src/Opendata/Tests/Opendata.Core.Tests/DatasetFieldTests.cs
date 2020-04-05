using Xunit;

namespace Opendata.Core.Tests
{
    public class DatasetFieldTests
    {
        public class MatrixTheoryData : TheoryData
        {
            public MatrixTheoryData()
            {
                AddRow("District,District name,Наименование района,string", "District", "District name",
                    "Наименование района", "string");
                AddRow(
                    "Number1,The number of citizens registered for the purpose of providing land,\"Количество  граждан, поставленных  на учет  в целях  предоставления земельного участка\",integer",
                    "Number1",
                    "The number of citizens registered for the purpose of providing land",
                    "\"Количество  граждан, поставленных  на учет  в целях  предоставления земельного участка\"",
                    "integer"
                );
                AddRow("Provision,Provision of land plots, %,\"Обеспеченность земельными участками, %\",float",
                    "Provision",
                    "Provision of land plots, %",
                    "\"Обеспеченность земельными участками, %\"",
                    "float");
            }
        }

        public static MatrixTheoryData MatrixData = new MatrixTheoryData();

        [Theory]
        [MemberData(nameof(MatrixData))]
        public void ParseLine(string line, string name, string eng, string rus, string format)
        {
            var field = new DatasetField();
            field.ParseLine(line);

            Assert.Equal(name, field.Name);
            Assert.Equal(eng, field.EngDescription);
            Assert.Equal(rus, field.RusDescription);
            Assert.Equal(format, field.Format);
        }
    }
}