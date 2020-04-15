using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Opendata.Web.Client.Model
{
    public class Region
    {
        public Region(string code, string title)
        {
            Code = code;
            Title = title;
        }

        [JsonPropertyName("code")]
        public string Code { get; }
        
        [JsonPropertyName("title")]
        public string Title { get; }

        public override bool Equals(object? obj)
        {
            return Code.Equals((obj as Region)?.Code);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Code} - {Title}";
        }
        
       public sealed class Comparer: IComparer<Region> 
       {
           public int Compare(Region x, Region y)
           {
               return string.Compare(x.Code, y.Code, StringComparison.InvariantCultureIgnoreCase);
           }
       }

       
    }
}