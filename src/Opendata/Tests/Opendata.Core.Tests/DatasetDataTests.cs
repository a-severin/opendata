using System.Collections.Generic;
using Xunit;

namespace Opendata.Core.Tests
{
    public class DatasetDataTests
    {
        [Fact]
        public void ParseContent()
        {
            var data = new DatasetData();
            var content =
                "Period;Wage\r\n1 квартал 2018 года;11228,4\r\n2 квартал 2018 года;11600,4\r\n3 квартал 2018 года;11967,6\r\n4 квартал 2018 года;11677,2\r\n* размер регионального МРОТ в каждом текущем квартале;\r\nустанавливается в размере 1, 2 величины прожиточного минимума;\r\nтрудоспособного населения Волгоградской области , определенного;\r\nпостановлением Администрации Волгоградской области об;\r\nустановлении величины прожиточного минимума на душу населения;\r\nи по основным социально-демографическим группам населения;\r\nВолгоградской области за последний истекший квартал  и;\r\nприменяется с даты официального опубликования постановления;";
            var fields = new List<DatasetField>
            {
                new DatasetField {Name = "Period"},
                new DatasetField {Name = "Wage"}
            };
            data.ParseContent(content, fields);

            Assert.True(string.IsNullOrEmpty(data.ErrorComment));
            Assert.Equal(4, data.Rows.Count);
        }
    }
}