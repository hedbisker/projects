using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class DataManager
    {
        private readonly string pathForProgram = $@"{DriveInfo.GetDrives()[0].Name}MoviesOnlineWatcher";
        private readonly string blackListPath = $@"{DriveInfo.GetDrives()[0].Name}MoviesOnlineWatcher\blackList";
        private readonly string allMoviesPath = $@"{DriveInfo.GetDrives()[0].Name}MoviesOnlineWatcher\DBfileForAllMovies";




    }
}
