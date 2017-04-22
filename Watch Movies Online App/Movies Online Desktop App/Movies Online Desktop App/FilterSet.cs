using Moves_Online_Downloader;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies_Online_Desktop_App
{
    public class FilterSet
    {
        public ConcurrentBag<Movie> Tittle { get; set; }
        public ConcurrentBag<Movie> Subtittles { get; set; }
        public ConcurrentBag<Movie> Categories { get; set; }
        public ConcurrentBag<Movie> Year { get; set; }
        public ConcurrentBag<Movie> Rate { get; set; }
        public ConcurrentBag<Movie> Type { get; set; }
        public ConcurrentBag<Movie> Length { get; set; }
        public Dictionary<string,int> counter { get; set; }
    }
}
