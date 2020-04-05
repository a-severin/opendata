using System;
using System.Collections.Generic;
using System.Linq;

namespace Opendata.Core
{
    public class DatasetData
    {
        public int Id { get; set; }
        public int StructureId { get; set; }
        public string ErrorComment { get; set; }
        public List<DatasetRow> Rows { get; set; }

        public void ParseContent(string content, List<DatasetField> fields)
        {
            try
            {
                var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                Rows = new List<DatasetRow>(lines.Length);
                if (lines.Length <= 1)
                {
                    ErrorComment = "Empty data";
                    return;
                }

                var header = lines[0];
                var chars = header.ToCharArray();
                var commaCount = chars.Count(_ => _ == ',');
                var semicolonCount = chars.Count(_ => _ == ';');

                var separator = commaCount > semicolonCount ? ',' : ';';

                for (var i = 1; i < lines.Length; i++)
                {
                    var line = lines[i]
                        .Replace("\n", "")
                        .Replace("\r", "");

                    if (!line.Contains(separator))
                    {
                        continue;
                    }

                    if (line.StartsWith('*'))
                    {
                        break;
                    }

                    var row = new DatasetRow();
                    row.ParseLine(line, separator, fields);

                    Rows.Add(row);
                }
            }
            catch (Exception e)
            {
                ErrorComment = e.Message;
            }
        }
    }
}