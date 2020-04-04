using System;

namespace Opendata.Core
{
    public class DatasetInput
    {
        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string RawData { get; set; }
        public bool IsProcessed { get; set; }
        public string MetaDataUrl { get; set; }
        public string ErrorComment { get; set; }
        public string SubsetCode { get; set; }
        public string SubsetName { get; set; }

        public void ParseLine(string line)
        {
            try
            {
                var firstCommaSeparator = line.IndexOf(',');
                SubsetCode = line.Substring(0, firstCommaSeparator);

                var urlStart = line.IndexOf("http", StringComparison.InvariantCultureIgnoreCase);
                var nameLength = urlStart - firstCommaSeparator - 2;
                SubsetName = nameLength > 0 ? line.Substring(firstCommaSeparator + 1, nameLength) : "Unkown";

                var urlPart = line.Substring(urlStart);
                MetaDataUrl = urlPart.Remove(urlPart.IndexOf(','));
            }
            catch (Exception e)
            {
                ErrorComment = $"Fail parse line: {e.Message}";
            }
        }
    }
}