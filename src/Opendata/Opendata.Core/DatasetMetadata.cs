using System;
using System.Collections.Generic;
using System.Linq;

namespace Opendata.Core
{
    public class DatasetMetadata
    {
        public string DataUrl { get; set; }
        public string ErrorComment { get; set; }
        public int Id { get; set; }
        public int InputId { get; set; }
        public bool IsProcessed { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public string StructureUrl { get; set; }

        public void ParseContent(string content)
        {
            try
            {
                var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                Properties = new Dictionary<string, string>(lines.Length - 1);
                if (lines.Length <= 1)
                {
                    ErrorComment = "Empty metadata";
                    return;
                }

                var dataUrl = new List<string>();
                var structureUrl = new List<string>();

                for (var i = 1; i < lines.Length; i++)
                {
                    var line = lines[i];

                    var firstCommaPosition = line.IndexOf(',');
                    var name = line.Substring(0, firstCommaPosition);
                    var value = line.Substring(firstCommaPosition + 1).Trim().TrimEnd('\r');
                    Properties[name] = value;

                    if (name.StartsWith("data-"))
                    {
                        dataUrl.Add(value);
                    }
                    else if (name.StartsWith("structure-"))
                    {
                        structureUrl.Add(value);
                    }
                }

                if (dataUrl.Any())
                {
                    DataUrl = dataUrl.First();
                }
                else
                {
                    throw new Exception("No data url founded");
                }

                if (structureUrl.Any())
                {
                    StructureUrl = structureUrl.First();
                }
                else
                {
                    throw new Exception("No structure url founded");
                }
            }
            catch (Exception e)
            {
                ErrorComment = e.Message;
            }
        }
    }
}