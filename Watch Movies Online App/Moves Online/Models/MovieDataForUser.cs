using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Moves_Online_Downloader
{
    public class MovieDataForUser
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Length { get;  set; }
        public string Categories { get; set; }
        public string Rate { get; set; }
        public string Subtittles { get; set; }
        public MovieDataForUser(string title, int year, string length, string categories, double rate,string subtittles)
        {
            Title = title;
            Year = year.ToString();
            Length = length;
            Categories = categories;
            Rate = rate.ToString();
            Subtittles = subtittles;
        }

        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            MovieDataForUser p = (MovieDataForUser)obj;

            PropertyInfo[] objProperties = p.GetType().GetProperties(),
                thisProperties = GetType().GetProperties();
            for (int i = 0; i < objProperties.Count(); i++)
            {
                if ((objProperties[i] == null && thisProperties[i] != null) || (objProperties[i] != null && thisProperties[i] == null) ||
                    (objProperties[i] != null && thisProperties[i] != null && !objProperties[i].Equals(thisProperties[i])))
                {
                    return false;
                }
            }
            return true;
        }
        public override int GetHashCode()
        {
            return Title.GetHashCode() ^ Length.GetHashCode() ^ Year.GetHashCode() ^
                Categories.GetHashCode() ^ Rate.GetHashCode() ^ Subtittles.GetHashCode();
        }
    }
}
