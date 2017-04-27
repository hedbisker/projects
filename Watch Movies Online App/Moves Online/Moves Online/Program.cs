using Movies_Online_Desktop_App;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moves_Online_Downloader
{
    class Program
    {
        private static ConcurrentBag<Movie> _moviesThreadSafe = new ConcurrentBag<Movie>();
        private object updateViewLock = new object();
        private static Dictionary<string, bool> botsIsRuning = new Dictionary<string, bool> { { "SubsMoviesBot", true }, { "PopcorenTimeBot", true } };
        private static object updateMainListLocker = new object();
        private static DataManager dataManager = new DataManager();
        static void Main(string[] args)
        {
            new SubsMoviesBot().Download(Done, "1935-" + DateTime.Now.Year, "", "");
            new PopcorenTimeBot().Download(Done, "1935-" + DateTime.Now.Year, "", "");
            while (isKeepDownload)
            {
                Thread.Sleep(10000);
            }
            Console.WriteLine("the movies are saved");
            Console.WriteLine("press any key to finish");
            //Console.ReadLine();
        }
        static bool isKeepDownload = true;
        static public void Done(string v, ConcurrentBag<Movie> tempList)
        {
            lock (updateMainListLocker)
            {
                _moviesThreadSafe.AddRange(tempList);
                botsIsRuning[v] = false;
                if (botsIsRuning.Values.All(bot => bot == false))
                {
                    dataManager.DefineLocalSpace();
                    dataManager.UpdateMovieList(_moviesThreadSafe.ToList());
                    isKeepDownload = false;
                }
            }
        }
    }
}