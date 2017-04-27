using HtmlAgilityPack;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Moves_Online_Downloader
{
    public class SubsMoviesBot : ICommonRobot
    {
        private bool[] FinishArray;
        private List<Thread> thList;
        private ConcurrentBag<Movie> _movies;
        private object o = new object();
        private HtmlManager htmlManager;
        private string _categoryText,
            _yearText,
            _subtittleText;
        private static int page, size;

        Thread updateListToBindingList;

        private static object obj = new object();
       
        private string makeUrl(string url, object page,int year)
        {
            if ((_categoryText.Length == 0) && (year == 0))
            {
                return url + "?page=" + Convert.ToString(page);
            }
            else if (_categoryText.Length == 0)
            {
                return url + "watch?year=" + year + "&page=" + Convert.ToString(page);
            }
            else if (year == 0)
            {
                return url + "watch?category=" + _categoryText + "&page=" + Convert.ToString(page);
            }
            return url + "watch?year=" + year + "&category=" + _categoryText + "&page=" + Convert.ToString(page);
        }

        Thread _TMain = null;
        public override void Download(Action<string, ConcurrentBag<Movie>> done, string yearText, string categoryText, string subtittleText) {

            _yearText = yearText;
            _categoryText = categoryText;
            _subtittleText = subtittleText;

            page = 1; size = 200;
            if ((_yearText.Length == 0) && (_yearText.Length == 0))
            {
                page++;
            }
            
            FinishArray = new bool[1];
            Array.Clear(FinishArray, 0, FinishArray.Length);
          
            if (updateListToBindingList != null)
            {
                updateListToBindingList.Abort();
            }
            if(_TMain != null)
            {
                _TMain.Abort();
            }
            thList = new List<Thread>();
            _movies = new ConcurrentBag<Movie>();
           
            htmlManager = new HtmlManager(_yearText, _categoryText);

            updateListToBindingList = new Thread(delegate ()
            {
                int lastCount = -1;
                while (true)
                {
                    if (_movies.Count == lastCount)
                    {
                        Thread.Sleep(500);
                    }
                    else
                    {
                        lastCount = _movies.Count;
                        Thread.Sleep(500);
                        if (FinishArray.All(b => b == true))
                        {
                            Console.WriteLine($"Finish");
                            done("SubsMoviesBot", _movies);
                            return;
                        }
                    }
                }
            });

            updateListToBindingList.IsBackground = true;
            updateListToBindingList.Start();
            int maxYear,startYear;
            if (_yearText.Length != 0)
            {
                string[] years = _yearText.Split('-');
                maxYear = years.Count() == 2 ? Convert.ToInt32(years.Last()) : Convert.ToInt32(years.First());
                startYear = years.Any() ? Convert.ToInt32(years[0]) : Convert.ToInt32(years);
            }else
            {
                maxYear = 0;
                startYear = 0;
            }

            Thread th = new Thread(() =>
            {

                FinishArray = new bool[(maxYear + 1) - startYear];
                Array.Clear(FinishArray, 0, FinishArray.Length);
                Parallel.For(startYear, (maxYear + 1), ((year) =>
                {
                    string url = "http://www.subsmovies.com/";
                    string urlInUse = makeUrl(url, page, year).DownloadStringHtmlPageFromUrl();

                    HtmlDocument docTest = new HtmlDocument();
                    docTest.LoadHtml(urlInUse);
                    HtmlNode siteTest = docTest.GetElementbyId("full_site");
                    if (siteTest == null)
                    {
                        Finish(startYear, maxYear, year);
                        return;
                    }
                    List<HtmlNode> maxPage = siteTest.Search("class", "pages_number_info");
                    int maxForTest = Convert.ToInt32(maxPage[0].InnerText.Split(' ')[3]);

                    Parallel.For(page, maxForTest, ((index) =>
                    {
                        string htmlPage = makeUrl(url, index, year).DownloadStringHtmlPageFromUrl();
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(Convert.ToString(htmlPage));

                        HtmlNode site = doc.GetElementbyId("full_site");

                        List<HtmlNode> section = site.Search("class", htmlManager.AlbumsSectionClassName);
                        List<HtmlNode> albums = section[0].Search("class", "album_holder");
                        int start = 0;
                        Parallel.For(start, albums.Count, ((i) =>
                         {
                             string specificMovieDataHolderInfoString = (url + albums[(int)i].Attributes["href"].Value).DownloadStringHtmlPageFromUrl();
                             HtmlDocument specificMovieDataHolderInfoSite = new HtmlDocument();
                             specificMovieDataHolderInfoSite.LoadHtml(specificMovieDataHolderInfoString);
                             HtmlNode site2 = specificMovieDataHolderInfoSite.GetElementbyId("full_site");

                             List<HtmlNode> availableSubtitlesBody = site2.Search("class", "available_subtitles_body");
                             string subsList = availableSubtitlesBody[0].InnerText.ToLower().Replace("\t", string.Empty).Replace("\n\n", string.Empty).Replace("subtitles", string.Empty);
                             if (_subtittleText.Equals("") || subsList.Contains(_subtittleText.ToLower()))
                             {
                                 List<HtmlNode> specificMovieDataHolderInfo =  site2.Search("class", "specific_movie_data_holder_info");
                                 HtmlNodeCollection movieInfo = specificMovieDataHolderInfo[0].ChildNodes;

                                 double rate;
                                 string imdbId = $"tt{albums[(int)i].Attributes["href"].Value.Split("=").Last()}";
                                 ImdbObject imdbMovie = ImdbObject.downloadImdbObjectById(imdbId);
                                 if (imdbMovie.Response.EqualsValue("False"))
                                 {
                                     return;
                                 }
                                 if (double.TryParse(imdbMovie.imdbRating, out rate) == false)
                                 {
                                     rate = 0;
                                 }
                                 

                                 Movie m = new Movie(imdbMovie.Title, Int32.Parse(Regex.Replace(imdbMovie.Year, @"[^\d]",string.Empty)),
                                     NormalizationLength(imdbMovie.Runtime), imdbMovie.Genre, rate, IsShowType(movieInfo[1].InnerText),
                                     movieInfo[17].ChildNodes[1].Attributes["data-href"].Value, subsList, imdbMovie.imdbID, imdbMovie.Poster);

                                 //Movie m = new Movie(movieInfo[1].InnerText, Int32.Parse(movieInfo[3].InnerText.Split().Last()),
                                 //    NormalizationLength(movieInfo[5].InnerText.Split().Skip(1).Join("")),
                                 //    movieInfo[7].InnerText.Split().Skip(1).Join(" "), rate, IsShowType(movieInfo[1].InnerText),
                                 //    movieInfo[17].ChildNodes[1].Attributes["data-href"].Value, subsList, imdbMovie.imdbID
                                 //    ,);

                                 _movies.Add(m);

                                 Console.WriteLine($"downloaded movie from SubsMovies {m.Title} {m.Year}" );

                             }
                         }));

                    }));
                    Finish(startYear,maxYear,year);
                }));
            });
            th.IsBackground = true;
            th.Start();
           
        }
        private Regex typeFinder = new Regex(@"(s\d+ e\d+|e\d+ s\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        protected char IsShowType(string name)
        {
            return typeFinder.IsMatch(name) ? 's' : 'm';
        }

        private void Finish(int startYear,int maxYear,int year)
        {
            lock (obj)
            {
                if ((FinishArray.Count() == (maxYear + 1) - startYear))
                {
                    FinishArray[(int)(year - startYear)] = true;
                }
            }
        }
    
    }
}
