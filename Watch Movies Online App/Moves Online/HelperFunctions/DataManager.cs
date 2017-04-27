using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moves_Online_Downloader;

namespace Movies_Online_Desktop_App
{
    public class DataManager
    {
        private readonly string pathForProgram = $@"{DriveInfo.GetDrives()[0].Name}MoviesOnlineWatcher";
        private readonly string blackListPath = $@"{DriveInfo.GetDrives()[0].Name}MoviesOnlineWatcher\blackList";
        private readonly string allMoviesPath = $@"{DriveInfo.GetDrives()[0].Name}MoviesOnlineWatcher\DBfileForAllMovies";

        public void DefineLocalSpace()
        {
            if (!Directory.Exists(pathForProgram))
            {
                Directory.CreateDirectory(pathForProgram);
            }
        }

        public void UpdateBlackList(List<Movie> _blackList)
        {
            File.WriteAllText(blackListPath, JsonConvert.SerializeObject(_blackList));
        }

        public void UpdateMovieList(List<Movie> _movies)
        {
            File.WriteAllText(allMoviesPath, JsonConvert.SerializeObject(_movies));
        }


        public List<Movie> GetAllMovies()
        {
            List<Movie> movieList = null;
            if (File.Exists(allMoviesPath))
            {
                movieList = File.ReadAllText(allMoviesPath).FromJsonToObject<List<Movie>>();
            }
            if(movieList == null)
            {
                return new List<Movie>();
            }
            return movieList;
        }

        public List<Movie> GetBlackList()
        {
            List<Movie> _blackList = new List<Movie>();
            if (File.Exists(blackListPath))
            {
                 _blackList = File.ReadAllText(blackListPath).FromJsonToObject<List<Movie>>();
                if (_blackList == null)
                {
                    _blackList = new List<Movie>();
                }
            }
            else
            {
                File.Create(blackListPath);
            }
            return _blackList;
        }
    }
}
