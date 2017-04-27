using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Moves_Online_Downloader
{
    public class Movie : IComparable
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int Length { get; set; }
        public string Categories { get; set; }
        public double Rate { get; set; }
        public string Url { get; set; }
        public char Type { get; set; }
        public string Subtittles { get; set; }
        public string ImdbId { get; set; }
        public string ImageLink { get; set; }
        public MovieDataForUser movieDataForUser { get; set; }
        public Movie(string title, int year, int length, string categories, double rate,char type, 
            string url,string subtittles,string imdbId,string imageLink)
        {
            Subtittles = subtittles;
            Type = type;
            string len = (length % 60 == 0) ? $"{length / 60}h" : 
                               (length < 60 ? $"{length % 60}min" : $"{length / 60}h {length % 60}min");
            movieDataForUser = new MovieDataForUser(title, year, len, categories, rate, subtittles);
            Title = title;
            Year = year;
            Length = length;
            Rate = rate;
            Categories = categories;
            Url = url;
            ImdbId = imdbId;
            ImageLink = imageLink;
        }
        public Movie(){}
        //public MovieDataForUser GetMovieDataForUser() { return movieDataForUser; } 
        
        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Movie p = (Movie)obj;

            if (p.Url == null && Url == null)
            {
                return true;
            }

            if ((p.Url == null) ^ (Url == null))
            {
                return false;
            }

            return Url.Equals(Url);

            //if (p.Title == null && Title != null ||
            //   p.Title != null && Title == null)
            //{
            //    return false;
            //}
            //PropertyInfo[] objProperties = p.GetType().GetProperties(),
            //    thisProperties = GetType().GetProperties();
            //for (int i = 0; i < objProperties.Count(); i++)
            //{
            //    if ((objProperties[i] == null && thisProperties[i] != null) || (objProperties[i] != null && thisProperties[i] == null) ||
            //        (objProperties[i] != null && thisProperties[i] != null && !objProperties[i].Equals(thisProperties[i])))
            //    {
            //        return false;
            //    }
            //}
            //return true;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode() ^ Length.GetHashCode() ^ Year.GetHashCode() ^
                Categories.GetHashCode() ^ Rate.GetHashCode() ^ Url.GetHashCode() ^
                ImdbId.GetHashCode() ^ ImageLink.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return -1;

            Movie p = (Movie)obj;

            if (p.Url == null && Url == null)
            {
                return 0;
            }

            if ((p.Url == null) && (Url != null))
            {
                return 1;
            }
            if ((p.Url != null) && (Url == null))
            {
                return -1;
            }
            return Url.CompareTo(Url);
        }
    }
}
