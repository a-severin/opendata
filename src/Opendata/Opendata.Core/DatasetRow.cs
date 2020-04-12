using System.Collections.Generic;
using System.Text;

namespace Opendata.Core
{
    public class DatasetRow
    {
        public Dictionary<string, string> Columns { get; set; }

        public void ParseLine(string line, char separator, List<DatasetField> fields)
        {
            Columns = new Dictionary<string, string>(fields.Count);
            var sb = new StringBuilder();
            var index = 0;
            var opendQuote = false;
            foreach (var c in line.ToCharArray())
            {
                if (c == '"' || c == '\'')
                {
                    opendQuote = !opendQuote;
                    sb.Append(c);
                    continue;
                }

                if (c == separator)
                {
                    if (opendQuote)
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        if (index < fields.Count)
                        {
                            var field = fields[index];
                            Columns[field.Name] = sb.ToString().TrimEnd('\r');
                        }

                        sb.Clear();
                        index++;
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }

            if (index < fields.Count)
            {
                var field = fields[index];
                Columns[field.Name] = sb.ToString();
                sb.Clear();
                index++;
            }

            for (var i = index; i < fields.Count; i++)
            {
                var field = fields[i];
                Columns[field.Name] = sb.ToString();
            }
        }
    }
}