using System.Collections.Generic;
using Xunit;

namespace Opendata.Core.Tests
{
    public class DatasetRowTests
    {
        [Fact]
        public void ParseLine_Case1()
        {
            var row = new DatasetRow();
            var line = "1 квартал 2018 года;11228,4";
            var fields = new List<DatasetField>
            {
                new DatasetField {Name = "Period"},
                new DatasetField {Name = "Wage"}
            };
            row.ParseLine(line, ';', fields);

            Assert.Equal(2, row.Columns.Count);
            Assert.Equal("1 квартал 2018 года", row.Columns["Period"]);
            Assert.Equal("11228,4", row.Columns["Wage"]);
        }

        [Fact]
        public void ParseLine_Case2()
        {
            var row = new DatasetRow();
            var line =
                "За государственную регистрацию транспортных средств и совершение иных регистрационных действий, связанных с выдачей государственных регистрационных знаков на мототранспортные средства, прицепы, тракторы, самоходные дорожно-строительные и иные самоходные машины, в том числе взамен утраченных или пришедших в негодность;1500 руб.";
            var fields = new List<DatasetField>
            {
                new DatasetField {Name = "Kind"},
                new DatasetField {Name = "Sum"}
            };
            row.ParseLine(line, ';', fields);

            Assert.Equal(2, row.Columns.Count);
            Assert.Equal(
                "За государственную регистрацию транспортных средств и совершение иных регистрационных действий, связанных с выдачей государственных регистрационных знаков на мототранспортные средства, прицепы, тракторы, самоходные дорожно-строительные и иные самоходные машины, в том числе взамен утраченных или пришедших в негодность",
                row.Columns["Kind"]);
            Assert.Equal("1500 руб.", row.Columns["Sum"]);
        }

        [Fact]
        public void ParseLine_Case3()
        {
            var row = new DatasetRow();
            var line = ";на 01.01.2020;;";
            var fields = new List<DatasetField>
            {
                new DatasetField {Name = "Name"},
                new DatasetField {Name = "Date"},
                new DatasetField {Name = "Place"},
                new DatasetField {Name = "Type"}
            };
            row.ParseLine(line, ';', fields);

            Assert.Equal(4, row.Columns.Count);
            Assert.Equal("", row.Columns["Name"]);
            Assert.Equal("на 01.01.2020", row.Columns["Date"]);
            Assert.Equal("", row.Columns["Place"]);
            Assert.Equal("", row.Columns["Type"]);
        }
    }
}