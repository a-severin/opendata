using System;
using System.Collections.Generic;

namespace Opendata.Core
{
    public class DatasetStructure
    {
        public int Id { get; set; }
        public int MetadataId { get; set; }
        public string ErrorComment { get; set; }
        public bool IsProcessed { get; set; }
        public List<DatasetField> Fields { get; set; }

        public void ParseContent(string content)
        {
            try
            {
                var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                Fields = new List<DatasetField>();
                if (lines.Length <= 1)
                {
                    ErrorComment = "Empty structure";
                    return;
                }

                for (var i = 1; i < lines.Length; i++)
                {
                    var line = lines[i];
                    var field = new DatasetField();
                    field.ParseLine(line);

                    Fields.Add(field);
                }
            }
            catch (Exception e)
            {
                ErrorComment = e.Message;
            }
        }
    }
}