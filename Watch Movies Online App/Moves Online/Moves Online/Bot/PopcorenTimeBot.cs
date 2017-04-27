using HtmlAgilityPack;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Moves_Online_Downloader.Models.ModelPopcorenTime;

namespace Moves_Online_Downloader
{
    public class PopcorenTimeBot : ICommonRobot
    {
        private string mainUrl = "https://api.apidomain.info/list?quality=720p,1080p,3d&";
        private ConcurrentBag<Movie> _movies = new ConcurrentBag<Movie>();
        private string _yearText, _categoryText, _subtittleText;
        private string movieLink = "https://popcorntime-online.io/";
        private int _maxYear, _minYear;
        private string makeUrl()
        {
            if (_categoryText.Equals(string.Empty))
            {
                return mainUrl + (_subtittleText.Equals(string.Empty) ? "" : "subtitles/" + _subtittleText.ToLower() + "/movie");
            }
            else
            {
                return mainUrl + "genre=" + _categoryText + "&";
            }
        }

        List<Rootobject> rootModel = new List<Rootobject>();
        List<string> rootModelStrings = new List<string>();
        private object threadLocker;
        public override void Download(Action<string, ConcurrentBag<Movie>> done, string yearText,
            string categoryText, string subtittleText)
        {
            threadLocker = new object();
            _yearText = yearText;
            _categoryText = categoryText;
            _subtittleText = subtittleText;
           
            int[] years = HelperFunctions.FeachLimitsFromStr(_yearText,1900,DateTime.Now.Year);
            _maxYear = years.Last();
            _minYear = years.First();
            makeUrl();
            int page = 1;

            Thread th = new Thread(delegate ()
            {
                bool _done = false;
                int failDownloadCounter = 0;
                while (!_done)
                {
                    if (failDownloadCounter <= 200)
                    {
                        (new Thread(new ParameterizedThreadStart(
                                    delegate (object _page)
                                    {
                                        failDownloadCounter++;
                                        int intPage = (int)_page;
                                        try
                                        {
                                            string jsonMovie = (new WebClient()).DownloadString(mainUrl + "page=" + intPage);
                                            if (jsonMovie.ToLower().Equals("{\"MovieList\":[]}".ToLower()))
                                            {
                                                _done = true;
                                            }
                                            else
                                            {
                                                rootModelStrings.Add(jsonMovie);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"fail download => { ex.Message}");
                                        }
                                        failDownloadCounter--;
                                    }))).Start(page);
                        page++;
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
                Parallel.ForEach(rootModelStrings, jsonMovie =>
                {
                    Rootobject rootObject = jsonMovie.FromJsonToObject<Rootobject>();

                        Parallel.ForEach(rootObject.MovieList, movie =>
                        {
                            string s = movie.genres.Join(" | ");
                            string link = movieLink + movie.title.Replace(" ", "-").Replace("(", "").Replace(")", "") + ".html?imdb=" + movie.id;
                            if (_minYear <= movie.year && movie.year <= _maxYear)
                            {
                                if (movie.genres.Select(name => name.ToLower()).Contains(_categoryText.ToLower()) || string.IsNullOrEmpty(_categoryText))
                                {
                                    ImdbObject imdbMovie = ImdbObject.downloadImdbObjectById(movie.imdb);

                                    double rate;
                                    if (double.TryParse(imdbMovie.imdbRating, out rate) == false)
                                    {
                                        rate = 0;
                                    }
                                    
                                    _movies.Add(new Movie(movie.title, movie.year, NormalizationLength(imdbMovie.Runtime),
                                        s, Math.Round(rate, 1),'m', link.ToLower(), "", imdbMovie.imdbID,imdbMovie.Poster));

                                    Console.WriteLine($"downloaded movie from Popcoren {movie.title} {movie.year}");
                                }
                            }
                        });
                });
                Thread.Sleep(2000);
                done("PopcorenTimeBot", _movies);
            });
            th.IsBackground = true;
            th.Start();
        }
    }
}
