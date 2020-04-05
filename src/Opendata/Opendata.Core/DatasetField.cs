using System;
using System.Text.RegularExpressions;

namespace Opendata.Core
{
    public class DatasetField
    {
        public string Name { get; set; }
        public string EngDescription { get; set; }
        public string RusDescription { get; set; }
        public string Format { get; set; }

        public void ParseLine(string line)
        {
            var firstCommaPosition = line.IndexOf(',');
            Name = line.Substring(0, firstCommaPosition);

            var lastCommadPosition = line.LastIndexOf(',');
            Format = line.Substring(lastCommadPosition + 1).TrimEnd('\r');

            var descriptionsPart = line
                .Substring(
                    firstCommaPosition,
                    lastCommadPosition - firstCommaPosition
                )
                .Trim(',');

            if (descriptionsPart.IndexOf(',') == descriptionsPart.LastIndexOf(','))
            {
                var descriptions = descriptionsPart.Split(',', StringSplitOptions.RemoveEmptyEntries);
                EngDescription = descriptions[0];
                RusDescription = descriptions[1];
            }
            else
            {
                var pattern = @"\p{IsCyrillic}";
                var match = Regex.Match(descriptionsPart, pattern);
                if (match.Success)
                {
                    var firstCyrillicCharPosition = match.Index;
                    var nearestCommadPosition = descriptionsPart.Substring(0, firstCyrillicCharPosition).LastIndexOf(',');
                    EngDescription = descriptionsPart
                        .Substring(
                            0,
                            nearestCommadPosition
                        )
                        .Trim(',');
                    RusDescription = descriptionsPart
                        .Substring(nearestCommadPosition)
                        .Trim(',');
                }
                else
                {
                    throw new Exception("Fail find rus description");
                }
            }
        }
    }
}