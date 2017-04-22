using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moves_Online_Downloader.Models
{
    public class ModelPopcorenTime
    {
        public class Rootobject
        {
            public Movielist[] MovieList { get; set; }
        }

        public class Movielist
        {
            public string id { get; set; }
            public int yts { get; set; }
            public string tmdb { get; set; }
            public string title { get; set; }
            public int year { get; set; }
            public float rating { get; set; }
            public string imdb { get; set; }
            public string actors { get; set; }
            public string writers { get; set; }
            public string directors { get; set; }
            public string trailer { get; set; }
            public string description { get; set; }
            public string poster_med { get; set; }
            public string poster_big { get; set; }
            public List<string> genres { get; set; }
            public List<Item> items { get; set; }
            public List<Items_Lang> items_lang { get; set; }
        }

        public class Item
        {
            public string torrent_url { get; set; }
            public string torrent_magnet { get; set; }
            public int torrent_seeds { get; set; }
            public int torrent_peers { get; set; }
            public string file { get; set; }
            public string quality { get; set; }
            public long size_bytes { get; set; }
            public string id { get; set; }
            public int type { get; set; }
            public string language { get; set; }
            public string subtitles { get; set; }
            public float durability { get; set; }
            public float vitality { get; set; }
        }

        public class Items_Lang
        {
            public string torrent_url { get; set; }
            public string torrent_magnet { get; set; }
            public int torrent_seeds { get; set; }
            public int torrent_peers { get; set; }
            public string file { get; set; }
            public string quality { get; set; }
            public long size_bytes { get; set; }
            public string id { get; set; }
            public int type { get; set; }
            public string language { get; set; }
            public string subtitles { get; set; }
            public float durability { get; set; }
            public float vitality { get; set; }
        }

    }
}
